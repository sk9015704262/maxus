using Dapper;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Maxus.Infrastructure.Repositories
{
    public class CustomerFeedbackRepository : ICustomerFeedBackRepository
    {
        private readonly IConfiguration _configuration;
        public CustomerFeedbackRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<tbl_CustomerFeedbackMaster> CreateAsync(tbl_CustomerFeedbackMaster obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Discription", obj.Description);
            parameters.Add("@Name", obj.Name);
            parameters.Add("@CompanyId", obj.CompanyId);
            parameters.Add("@IndustrySegmentId", obj.IndustrySegmentId);
            parameters.Add("@IsMandatory", obj.IsMandatory);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@CreatedBy", obj.CreatedBy);
            parameters.Add("@ChekListOption", obj.ChekListOption);    
            parameters.Add("@CustomerFeedbackId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateCustomerFeedback", parameters, commandType: CommandType.StoredProcedure))
                    {
                        if (multi == null)
                        {
                            throw new Exception("QueryMultipleAsync returned null.");
                        }

                        int customerFeedbackId = parameters.Get<int>("@CustomerFeedbackId");
                        int error = parameters.Get<int>("@Error");

                        if (error != 0)
                        {
                            throw new Exception("Visit already exists with the same name.");
                        }

                        obj.Id = customerFeedbackId;

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
                    parameters.Add("@CustomerFeedbackId", id);
                    parameters.Add("@UpdatedBy", 1);
                    parameters.Add("@UpdatedAt", DateTime.Now);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "DeleteCustomerfeedback",
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

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_CustomerFeedbackMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int companyId  , int? s)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchColumn", s);
            parameters.Add("SearchTerm", searchTerm ?? "");
            parameters.Add("CompanyId", companyId);
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetCustomerFeedback", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var customerFeedbacks = (await multi.ReadAsync<tbl_CustomerFeedbackMaster>()).ToList();
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

        public Task<(FilterRecordsResponse, IEnumerable<tbl_CustomerFeedbackMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm , int? i)
        {
            throw new NotImplementedException();
        }

        public async Task<tbl_CustomerFeedbackMaster?> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("GetCustomerFeedbackById", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var customerFeedback = await multi.ReadSingleOrDefaultAsync<tbl_CustomerFeedbackMaster>();

                        if (customerFeedback != null)
                        {
                            customerFeedback.CustomerCheklistOption = (await multi.ReadAsync<tbl_CustomerCheklistOption>()).ToList();
                        }

                        return customerFeedback;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting customer feedback by ID.", ex);
            }
        }

    

        public async Task<IEnumerable<tbl_CustomerFeedbackMaster>> GetCustomerFeedbackByCompanyAsync(int UserId, int CompanyId)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", UserId);
                parameters.Add("@CompanyId", CompanyId);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("GetCustomerFeedbackByCompany", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var customerFeedbackList = (await multi.ReadAsync<tbl_CustomerFeedbackMaster>()).ToList();

                        // Aggregate the data
                        var aggregatedData = customerFeedbackList
                   .GroupBy(feedback => feedback.Id)
                   .Select(g =>
                   {
                       var first = g.First();
                       return new tbl_CustomerFeedbackMaster
                       {
                           Id = first.Id,
                           Name = first.Name,
                           Description = first.Description,
                           IndustrySegmentId = first.IndustrySegmentId,
                           IndustrySegmentName = first.IndustrySegmentName,
                           IsMandatory = first.IsMandatory,
                           CompanyId = first.CompanyId,
                           CreatedAt = first.CreatedAt,
                           CreatedBy = first.CreatedBy,
                           cheklistOptions = g
                               .Select(feedback => new tbl_CheklistOption { Name = feedback.CheckListName , Id = feedback.CheckListId })
                               .Distinct()
                               .ToList(),
                           UpdatedAt = first.UpdatedAt,
                           UpdatedBy = first.UpdatedBy,
                           CreatedByName = first.CreatedByName,
                           UpdatedByName = first.UpdatedByName
                           
                       };
                   })
                   .ToList();

                        return aggregatedData;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting customer feedback.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_CustomerFeedbackMaster obj)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", obj.Id);
                    parameters.Add("@Discription", obj.Description);
                    parameters.Add("@Name", obj.Name);
                    parameters.Add("@CompanyId", obj.CompanyId);
                    parameters.Add("@IndustrySegmentId", obj.IndustrySegmentId);
                    parameters.Add("@IsMandatory", obj.IsMandatory);
                    parameters.Add("@UpdatedAt", obj.UpdatedAt);
                    parameters.Add("@ChekListOption", obj.ChekListOption);
                    parameters.Add("@UpdatedBy", obj.UpdatedBy);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "UpdateCustomerFeedback",
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
