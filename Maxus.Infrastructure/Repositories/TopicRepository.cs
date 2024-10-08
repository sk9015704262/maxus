using Dapper;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Maxus.Infrastructure.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly IConfiguration _configuration;

        public TopicRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tbl_TopicsMaster> CreateAsync(tbl_TopicsMaster obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", obj.CompanyId);
            parameters.Add("@IndustrySegmentId", obj.IndustrySegmentId);
            parameters.Add("@Name", obj.Name);
            parameters.Add("@Description", obj.Description);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@CreatedBy", obj.CreatedBy);

            parameters.Add("@TopicId", dbType: DbType.Int32, direction: ParameterDirection.Output);  // Output parameter
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);  // Output parameter

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateTopic", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int topicId = parameters.Get<int>("@TopicId");
                        int error = parameters.Get<int>("@Error");

                        if (error != 0)
                        {
                            throw new Exception("Topic already exists with the same name.");
                        }

                        obj.Id = topicId;

                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Topic is already created with the same name.", ex);
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
                    parameters.Add("@TopicId", id);
                    parameters.Add("@UpdatedBy", 1);
                    parameters.Add("@UpdatedAt", DateTime.Now);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "DeleteTopic",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Topic Delete Error In Repository.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_TopicsMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int? SearchColumn)
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

                    using (var multi = await connection.QueryMultipleAsync("sp_GetTopic", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var topics = (await multi.ReadAsync<tbl_TopicsMaster>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filterRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filterRecords
                        };

                        return (paginationResponse, topics);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Topic GetAll Error In Repository.", ex);
            }
        }

        public async Task<tbl_TopicsMaster?> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var topic = await connection.QuerySingleOrDefaultAsync<tbl_TopicsMaster>(
                        "GetTopicById",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return topic;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Topic GetById Error In Repository.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_TopicsMaster obj)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@TopicId", obj.Id);
                    parameters.Add("@CompanyId", obj.CompanyId);
                    parameters.Add("@IndustrySegmentId", obj.IndustrySegmentId);
                    parameters.Add("@Name", obj.Name);
                    parameters.Add("@Description", obj.Description);
                    parameters.Add("@UpdatedBy", obj.UpdatedBy);
                    parameters.Add("@UpdatedAt", obj.UpdatedAt);

                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "UpdateTopic",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Topic Update Error In Repository.", ex);
            }
        }
    }
}
