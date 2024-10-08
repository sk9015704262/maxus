using Dapper;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Maxus.Domain.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Maxus.Infrastructure.Repositories
{
    public class UserRightsRepository : IUserRightRepository
    {
        private readonly IConfiguration _configuration;

        public UserRightsRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<tbl_UserRights> CreateAsync(tbl_UserRights userRights)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", userRights.UserId);
            parameters.Add("@Type", userRights.RightTypeId);
            parameters.Add("@createdAt", userRights.CreatedAt);
            parameters.Add("@createdBy", userRights.CreatedBy);
            parameters.Add("@UserRightId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var detailsTable = new DataTable();
            detailsTable.Columns.Add("ID", typeof(long));
            foreach (var detail in userRights.Details)  // Assuming userRights.Details is IEnumerable<long>
            {
                detailsTable.Rows.Add(detail.Id);
            }
            parameters.Add("@Details", detailsTable.AsTableValuedParameter("dbo.IDArray"));

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateUserRights", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int userRightId = parameters.Get<int>("@UserRightId");
                        int error = parameters.Get<int>("@Error");

                        if (error != 0)
                        {
                            throw new Exception("User rights already exist with the same name.");
                        }

                        userRights.Id = userRightId;

                        return userRights;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create user rights.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_UserRights>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int? SearchColumn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SearchColumn", SearchColumn);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchTerm", searchTerm ?? "");
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetUserRights", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var UserRights = (await multi.ReadAsync<tbl_UserRights>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filterRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filterRecords
                        };

                        if (UserRights != null)
                        {
                            foreach (var UserRight in UserRights)
                            {
                                if (UserRight.RightTypeId == 1)
                                {
                                    UserRight.Rights = "Branch";
                                }

                                if (UserRight.RightTypeId == 2)
                                {
                                    UserRight.Rights = "Client";
                                }

                                if (UserRight.RightTypeId == 3)
                                {
                                    UserRight.Rights = "Site";
                                }
                             }
                        }

                        return (paginationResponse, UserRights);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("UserRight GetAll Error In Repository.", ex);
            }
        }

        public async Task<tbl_UserRights?> GetByIdAsync(int id)
        {
           


            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    //use QueryMultipleAsync
                    using (var multi = await connection.QueryMultipleAsync("sp_GetUserRightsById", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var UserRight = (await multi.ReadAsync<tbl_UserRights>()).FirstOrDefault();
                        List<tbl_UserRightsDetails> details = (await multi.ReadAsync<tbl_UserRightsDetails>()).ToList();
                        UserRight.Details = details;

                        if (UserRight != null)
                        {
                            if (UserRight.RightTypeId == 1)
                            {
                                UserRight.Rights = "Branch";
                            }

                            if (UserRight.RightTypeId == 2)
                            {
                                UserRight.Rights = "Client";
                            }

                            if (UserRight.RightTypeId == 3)
                            {
                                UserRight.Rights = "Site";
                            }
                        }

                        return UserRight;
                    }

                    

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Topic GetById Error In Repository.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_UserRights userRights)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@UserRightsId", userRights.UserId);
                    parameters.Add("@RightTypeId", userRights.RightTypeId);
                    parameters.Add("@CreatedAt", userRights.UpdatedAt);
                    parameters.Add("@CreatedBy", userRights.UpdatedBy);

                    var detailsTable = new DataTable();
                    detailsTable.Columns.Add("ID", typeof(long));
                    foreach (var detail in userRights.Details)  // Assuming userRights.Details is IEnumerable<long>
                    {
                        detailsTable.Rows.Add(detail.Id);
                    }

                    parameters.Add("@Details", detailsTable.AsTableValuedParameter("dbo.IDArray"));
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await dbConnection.ExecuteAsync("UpdateUserRightById", parameters, commandType: CommandType.StoredProcedure);

                    // Get the output parameter value
                    bool success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return false;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@UserRightsId", id);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await dbConnection.ExecuteAsync("DeleteUserRightsById", parameters, commandType: CommandType.StoredProcedure);

                    // Get the output parameter value
                    bool success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return false;
            }
        }
    }
}
