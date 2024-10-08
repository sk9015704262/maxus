using Dapper;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Maxus.Infrastructure.Repositories
{
    public class CustomerFeedbackOptionRepository : ICustomerFeedbackOptionRepository
    {
        private readonly IConfiguration _configuration;
        public CustomerFeedbackOptionRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<tbl_CustomerCheklistOption> CreateAsync(tbl_CustomerCheklistOption obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", obj.Name);
            parameters.Add("@CompanyId", obj.CompanyId);
            parameters.Add("@IsDefault", obj.IsDefault);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@CreatedBy", obj.CreatedBy);
            parameters.Add("@CustomerFeedbackOptionId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateCustomerFeedbackOption", parameters, commandType: CommandType.StoredProcedure))
                    {
                        if (multi == null)
                        {
                            throw new Exception("QueryMultipleAsync returned null.");
                        }

                        int customerFeedbackOptionId = parameters.Get<int>("@CustomerFeedbackOptionId");
                        int error = parameters.Get<int>("@Error");

                        if (error != 0)
                        {
                            throw new Exception("Visit already exists with the same name.");
                        }

                        obj.Id = customerFeedbackOptionId;

                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating visit.", ex);
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
                    parameters.Add("@CustomerFeedbackOptionId", id);
                    parameters.Add("@UpdatedBy", 1);
                    parameters.Add("@UpdatedAt", DateTime.Now);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "DeleteCustomerfeedbackOption",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting visit.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_CustomerCheklistOption>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm , int? SearchColumn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SearchColumn", SearchColumn);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchTerm", searchTerm ?? "");
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetCustomerFeedbaackOption", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var customerFeedbacks = (await multi.ReadAsync<tbl_CustomerCheklistOption>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filteredRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filteredRecords
                        };

                        return (paginationResponse, customerFeedbacks);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all visits.", ex);
            }
        }

        public async Task<tbl_CustomerCheklistOption?> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var customerFeedback = await connection.QuerySingleOrDefaultAsync<tbl_CustomerCheklistOption>(
                        "GetCustomerFeedbackOptionById",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return customerFeedback;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting visit by ID.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_CustomerCheklistOption obj)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", obj.Id);
                    parameters.Add("@Name", obj.Name);
                    parameters.Add("@CompanyId", obj.CompanyId);
                    parameters.Add("@IsDefault", obj.IsDefault);
                    parameters.Add("@UpdatedAt", obj.UpdatedAt);
                    parameters.Add("@UpdatedBy", obj.UpdatedBy);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "UpdateCustomerFeedbackOption",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating visit.", ex);
            }
        }
    }
}
