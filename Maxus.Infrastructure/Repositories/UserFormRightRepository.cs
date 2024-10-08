using Dapper;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Maxus.Infrastructure.Repositories
{
    public class UserFormRightRepository : IUserFormRepository
    {
        private readonly IConfiguration _configuration;

        public UserFormRightRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<tbl_UserFormRights> CreateAsync(tbl_UserFormRights userFormRights)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userFormRights.UserId);
            parameters.Add("@FormId", userFormRights.FormId);
            parameters.Add("@CanView", userFormRights.CanView);
            parameters.Add("@CanAdd", userFormRights.CanAdd);
            parameters.Add("@CanEdit", userFormRights.CanEdit);
            parameters.Add("@CanExport", userFormRights.CanExport);
            parameters.Add("@CanDelete", userFormRights.CanDelete);


            parameters.Add("@UserFormRightId", dbType: DbType.Int32, direction: ParameterDirection.Output);


            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateUserFormRight", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int userFormRightId = parameters.Get<int>("@UserFormRightId");

                        userFormRights.UserFormRightsId = userFormRightId;

                        return userFormRights;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("User form right is already created with the same name.", ex);
            }
        }

        public async Task<tbl_UserFormRights?> GetByIdAsync(int id, int formId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", id);
            parameters.Add("@FormId", formId);


            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                var userFormRights = await connection.QuerySingleOrDefaultAsync<tbl_UserFormRights>(
                    "GetUserFormRightByUserId",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return userFormRights;
            }
        }

        public async Task<bool> UpdateAsync(tbl_UserFormRights userFormRights)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@UserId", userFormRights.UserId);
                    parameters.Add("@FormId", userFormRights.FormId);
                    parameters.Add("@CanView", userFormRights.CanView);
                    parameters.Add("@CanAdd", userFormRights.CanAdd);
                    parameters.Add("@CanEdit", userFormRights.CanEdit);
                    parameters.Add("@CanExport", userFormRights.CanExport);
                    parameters.Add("@CanDelete", userFormRights.CanDelete);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "UpdateUserFormRights",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user form right.", ex);
            }
        }
    }
}
