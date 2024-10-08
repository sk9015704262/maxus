using Dapper;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.X86;

namespace Maxus.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tbl_Users> AdminLoginAsync(string Email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Username", Email);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    var user = await connection.QuerySingleOrDefaultAsync<tbl_Users>(
                        "logiAdmin",
                        new { Username = Email },
                        commandType: CommandType.StoredProcedure 
                    );

                    if (user != null)
                    {
                        user.UserType = "Admin";
                    }

                    return user;


                }
            }
            catch (Exception ex)
            {
                throw new Exception("Login User Error In Repository.", ex);
            }
        }


        public async Task<tbl_Users> UserLoginAsync(string Email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Username", Email);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    var user = await connection.QuerySingleOrDefaultAsync<tbl_Users>(
                        "loginuser",
                        new { Username = Email },
                        commandType: CommandType.StoredProcedure
                    );

                    if (user != null)
                    {
                        user.UserType = "User";
                    }

                    return user;


                }
            }
            catch (Exception ex)
            {
                throw new Exception("Login User Error In Repository.", ex);
            }
        }


    }
}
