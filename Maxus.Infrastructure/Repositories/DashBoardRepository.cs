using Dapper;
using Maxus.Domain.Entities;
using Maxus.Domain.Entities.PartialEntities;
using Maxus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxus.Infrastructure.Repositories
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly IConfiguration _configuration;

        public DashBoardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tbl_DashboardCount?> GetCount(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var Count = await connection.QuerySingleOrDefaultAsync<tbl_DashboardCount>("GetAdmindashboardData", parameters, commandType: CommandType.StoredProcedure);

                    return Count;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get Count by ID.", ex);
            }
        }
    }
}
