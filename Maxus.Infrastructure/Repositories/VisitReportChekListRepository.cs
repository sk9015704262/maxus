using Dapper;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Maxus.Infrastructure.Repositories
{
    public class VisitReportChekListRepository : IVisitReportChekListRepository
    {
        private readonly IConfiguration _configuration;

        public VisitReportChekListRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<tbl_VisitReportChekListMaster> CreateAsync(tbl_VisitReportChekListMaster obj)
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
            parameters.Add("@VisitId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateVisitReportChekList", parameters, commandType: CommandType.StoredProcedure))
                    {
                        if (multi == null)
                        {
                            throw new Exception("QueryMultipleAsync returned null.");
                        }

                        int visitId = parameters.Get<int>("@VisitId");
                        int error = parameters.Get<int>("@Error");

                        if (error != 0)
                        {
                            throw new Exception("Visit already exists with the same name.");
                        }

                        obj.Id = visitId;

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
                    parameters.Add("@VisitId", id);
                    parameters.Add("@UpdatedBy", 1);
                    parameters.Add("@UpdatedAt", DateTime.Now);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "[DeleteVisitReportChekList]",
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

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_VisitReportChekListMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int? SearchColumn)
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

                    using (var multi = await connection.QueryMultipleAsync("sp_GetVisitRepotChekList", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var visits = (await multi.ReadAsync<tbl_VisitReportChekListMaster>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filteredRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filteredRecords
                        };

                        return (paginationResponse, visits);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all visits.", ex);
            }
        }

        public async Task<tbl_VisitReportChekListMaster?> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("GetVisitReportChekListById", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var customerFeedback = await multi.ReadSingleOrDefaultAsync<tbl_VisitReportChekListMaster>();

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
                throw new Exception("Error getting visit by ID.", ex);
            }
        }

        public async Task<IEnumerable<tbl_VisitReportChekListMaster?>> GetVisitByComanyIdAsync(int userId, int companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@CompanyId", companyId);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("GetVisitReportsByCompany", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var visitReportList = (await multi.ReadAsync<tbl_VisitReportChekListMaster>()).ToList();

                        var groupedData = visitReportList
                            .GroupBy(vr => vr.Id)
                            .Select(g =>
                            {
                                var first = g.First();
                                return new tbl_VisitReportChekListMaster
                                {
                                    Id = first.Id,
                                    Name = first.Name,
                                    Description = first.Description,
                                    IndustrySegmentId = first.IndustrySegmentId,
                                    IndustrySegmentName = first.IndustrySegmentName,
                                    IsMandatory = first.IsMandatory,
                                    ChekListName = first.ChekListName,
                                    cheklistOptions = g
                                        .Select(vr => new tbl_CheklistOption { Name = vr.ChekListName , Id = vr.CheckListId })
                                        .Distinct()
                                        .ToList(),
                                    CompanyId = first.CompanyId,
                                    CreatedAt = first.CreatedAt,
                                    CreatedBy = first.CreatedBy,
                                    UpdatedAt = first.UpdatedAt,
                                    UpdatedBy = first.UpdatedBy,
                                    CreatedByName = first.CreatedByName,
                                    UpdatedByName = first.UpdatedByName
                                };
                            })
                            .ToList();

                        return groupedData;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all visits.", ex);
            }
        }

            public async Task<bool> UpdateAsync(tbl_VisitReportChekListMaster obj)
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
                    parameters.Add("@UpdatedBy", obj.UpdatedBy);
                    parameters.Add("@ChekListOption", obj.ChekListOption);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "UpdateVisitReport",
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
