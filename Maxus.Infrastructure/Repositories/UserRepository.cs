using Dapper;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Maxus.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<tbl_Users> CreateAsync(tbl_Users obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", obj.Name);
            parameters.Add("@PhoneNo", obj.PhoneNo ?? "");
            parameters.Add("@Email", obj.Email ?? "");
            parameters.Add("@Designation", obj.Designation);
            parameters.Add("@Username", obj.Username);
            parameters.Add("@Password", obj.Password);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@CreatedBy", obj.CreatedBy);
            parameters.Add("@CompanyIds", obj.CompanyId);
            parameters.Add("@RoleId", obj.RoleId);


            parameters.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    await connection.ExecuteAsync("CreateUser", parameters, commandType: CommandType.StoredProcedure);

                    var errorCode = parameters.Get<int>("@Error");
                    var userId = parameters.Get<int>("@UserId");

                    if (errorCode == 1 || userId == 0)
                    {
                        throw new Exception("This Email Already Exist");
                    }

                    obj.Id = userId;

                    return obj;
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
                    parameters.Add("@UserId", id);
                    parameters.Add("@UpdatedBy", 1);
                    parameters.Add("@UpdatedAt", DateTime.Now);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "sp_DeleteUser",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("User Delete Error In Repository.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_Users>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int? SearchColumn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchTerm", searchTerm);
            parameters.Add("SearchColumn", SearchColumn);
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetUsers", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var Users = (await multi.ReadAsync<tbl_Users>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filterRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filterRecords
                        };


                        if (Users != null)
                        {
                            foreach (var User in Users)
                            {
                                if (User.RoleId == 1)
                                {
                                    User.UserType = "Admin";
                                }

                                if (User.RoleId == 2)
                                {
                                    User.UserType = "User";
                                }
                            }
                        }

                            return (paginationResponse, Users);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("User GetAll Error In Repository.", ex);
            }
        }

        public async Task<tbl_Users?> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var User = await connection.QuerySingleOrDefaultAsync<tbl_Users>(
                        "GetUserById",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );



                   

                    return User;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Branch GetById Error In Repository.", ex);
            }
        }

        public async Task<bool> ResetPassword(long UserId, string Password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", UserId);  
            parameters.Add("@Password", Password);
            parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    await connection.ExecuteAsync(
                       "sp_ResetPassword",
                       parameters,
                       commandType: CommandType.StoredProcedure
                   );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("User Update Error In Repository.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_Users obj)
        {
            //
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", obj.Id);
            parameters.Add("@Name", obj.Name);
            parameters.Add("@PhoneNo", obj.PhoneNo ?? "");
            parameters.Add("@Email", obj.Email ?? "");
            parameters.Add("@Designation", obj.Designation);
            parameters.Add("@Username", obj.Username);
            parameters.Add("@Company", obj.CompanyId);
            parameters.Add("@UpdatedAt", obj.UpdatedAt);
            parameters.Add("@RoleId", obj.RoleId);
            parameters.Add("@UpdatedBy", obj.UpdatedBy);
            parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    await connection.ExecuteAsync(
                       "sp_UpdateUser",
                       parameters,
                       commandType: CommandType.StoredProcedure
                   );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("User Update Error In Repository.", ex);
            }

        }
    }
}
