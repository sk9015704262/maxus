using Dapper;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Maxus.Infrastructure.Repositories
{
    public class IndustrySegmentsRepository : IIndustrySegmentsRepository
    {
        private readonly IConfiguration _configuration;

        public IndustrySegmentsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tbl_IndustrySegments> CreateAsync(tbl_IndustrySegments obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Code", obj.Code);
            parameters.Add("@Name", obj.Name);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@CreatedBy", obj.CreatedBy);

            parameters.Add("@IndustrySegmentsId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateIndustrySegment", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int industrySegmentId = parameters.Get<int>("@IndustrySegmentsId");
                        int error = parameters.Get<int>("@Error");

                        if (error == 1)
                        {
                            throw new Exception("Entered Name Is Already Exist In System.");
                        }

                        if (error == 2)
                        {
                            throw new Exception("Entered Code Is Already Exist In System.");
                        }

                        obj.Id = industrySegmentId;

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
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@IndustrySegmentsId", id);
                parameters.Add("@UpdatedBy", 1);
                parameters.Add("@UpdatedAt", DateTime.Now);
                parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(
                    "DeleteIndustrySegments",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var success = parameters.Get<bool>("@Success");

                return success;
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_IndustrySegments>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int? SearchColumn)
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

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var multi = await connection.QueryMultipleAsync("sp_GetIndustrySegments", parameters, commandType: CommandType.StoredProcedure))
                {
                    var industrySegments = (await multi.ReadAsync<tbl_IndustrySegments>()).ToList();
                    var totalRecords = parameters.Get<int>("TotalRecords");
                    var filterRecords = parameters.Get<int>("FilterRecords");
                    var paginationResponse = new FilterRecordsResponse
                    {
                        TotalRecords = totalRecords,
                        FilteredRecords = filterRecords
                    };

                    return (paginationResponse, industrySegments);
                }
            }
        }

        public async Task<tbl_IndustrySegments?> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var industrySegments = await connection.QuerySingleOrDefaultAsync<tbl_IndustrySegments>(
                    "[GetIndustrySegmentsById]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return industrySegments;
            }
        }

        public async Task<bool> UpdateAsync(tbl_IndustrySegments obj)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@IndustrySegmentsId", obj.Id);
                parameters.Add("@CompanyCode", obj.Code);
                parameters.Add("@CompanyName", obj.Name);
                parameters.Add("@UpdatedBy", obj.UpdatedBy);
                parameters.Add("@UpdatedAt", obj.UpdatedAt);

                parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(
                    "UpdateIndustrySegments",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                var success = parameters.Get<bool>("@Success");

                return success;
            }
        }
    }
}
