using Dapper;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Maxus.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly IConfiguration _configuration;

        public BranchRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tbl_BranchMaster> CreateAsync(tbl_BranchMaster obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Code", obj.Code);
            parameters.Add("@CompanyId", obj.CompanyId);
            parameters.Add("@Name", obj.Name);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@CreatedBy", obj.CreatedBy);

            parameters.Add("@BranchId", dbType: DbType.Int32, direction: ParameterDirection.Output);  // Output parameter
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);  // Output parameter

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateBranch", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int branchId = parameters.Get<int>("@BranchId");
                        int error = parameters.Get<int>("@Error");

                        if (error == 1)
                        {
                            throw new Exception("Entered Branch Name Is Already Exist In System.");
                        }

                        if (error == 2)
                        {
                            throw new Exception("Entered Branch Code Is Already Exist In System.");
                        }

                        obj.Id = branchId;

                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message);
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
                    parameters.Add("@BranchId", id);
                    parameters.Add("@UpdatedBy", 1);
                    parameters.Add("@UpdatedAt", DateTime.Now);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "DeleteBranch",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return parameters.Get<bool>("@Success");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Branch Delete Error In Repository.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_BranchMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm , int? SearchColumn ) 
        {
            throw new NotImplementedException();
           
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_BranchMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, long CompanyId , int? SearchColumn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchColumn", SearchColumn);
            parameters.Add("SearchTerm", searchTerm);
            parameters.Add("CompanyId", CompanyId);
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetBranches", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var branches = (await multi.ReadAsync<tbl_BranchMaster>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filterRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filterRecords
                        };

                        return (paginationResponse, branches);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Branch GetAll Error In Repository.", ex);
            }
        }

        public async Task<tbl_BranchMaster> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    return await connection.QuerySingleOrDefaultAsync<tbl_BranchMaster>(
                        "GetBranchById",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Branch GetById Error In Repository.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_BranchMaster obj)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@BranchId", obj.Id);
                    parameters.Add("@CompanyId", obj.CompanyId);
                    parameters.Add("@CompanyCode", obj.Code);
                    parameters.Add("@CompanyName", obj.Name);
                    parameters.Add("@UpdatedBy", obj.UpdatedBy);
                    parameters.Add("@UpdatedAt", obj.UpdatedAt);

                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "UpdateBranch",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return parameters.Get<bool>("@Success");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Branch Update Error In Repository.", ex);
            }
        }
    }
}
