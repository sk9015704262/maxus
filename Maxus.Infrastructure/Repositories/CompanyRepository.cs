using Dapper;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Maxus.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IConfiguration _configuration;

        public CompanyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tbl_CompanyMaster> CreateAsync(tbl_CompanyMaster company)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Code", company.Code);
            parameters.Add("@Name", company.Name);
            parameters.Add("@CreatedAt", company.CreatedAt);
            parameters.Add("@CreatedBy", company.CreatedBy);
            parameters.Add("@CompanyId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateCompany", parameters, commandType: CommandType.StoredProcedure))
                    {
                        if (multi == null)
                        {
                            throw new Exception("QueryMultipleAsync returned null.");
                        }

                        int companyId = parameters.Get<int>("@CompanyId");
                        int error = parameters.Get<int>("@Error");

                        if (error == 1)
                        {
                            throw new Exception("Entered Company Name Is Already Exist In System.");
                        }

                        if (error == 2)
                        {
                            throw new Exception("Entered Company Code Is Already Exist In System.");
                        }

                        company.Id = companyId;

                        return company;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@CompanyId", id);
                    parameters.Add("@UpdatedBy", 1);
                    parameters.Add("@UpdatedAt", DateTime.Now);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "DeleteCompany",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting company.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_CompanyMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm , int? SearchColumn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchColumn", SearchColumn);
            parameters.Add("SearchTerm", searchTerm ?? "");
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetCompanies", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var companies = (await multi.ReadAsync<tbl_CompanyMaster>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filteredRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filteredRecords
                        };

                        return (paginationResponse, companies);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all companies.", ex);
            }
        }

        public async Task<tbl_CompanyMaster?> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var company = await connection.QuerySingleOrDefaultAsync<tbl_CompanyMaster>(
                        "GetCompanyById",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return company;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting company by ID.", ex);
            }
        }

        public async Task<IEnumerable<tbl_UserCompany>> GetCompanyByUser(long userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", userId);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("GetCompanyByUserId", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var companies = (await multi.ReadAsync<tbl_UserCompany>()).ToList();

                        return companies;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting companies by user.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_CompanyMaster company)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@CompanyId", company.Id);
                    parameters.Add("@CompanyCode", company.Code);
                    parameters.Add("@CompanyName", company.Name);
                    parameters.Add("@UpdatedBy", company.UpdatedBy);
                    parameters.Add("@UpdatedAt", company.UpdatedAt);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "UpdateCompany",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating company.", ex);
            }
        }
    }
}
