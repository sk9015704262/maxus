using Dapper;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Maxus.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IConfiguration _configuration;

        public ClientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tbl_ClientMaster> CreateAsync(tbl_ClientMaster obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", obj.CompanyId);
            parameters.Add("@Code", obj.Code);
            parameters.Add("@Name", obj.Name);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@CreatedBy", obj.CreatedBy);

            parameters.Add("@ClientId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateClient", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int clientId = parameters.Get<int>("@ClientId");
                        int error = parameters.Get<int>("@Error");

                        if (error != 0)
                        {
                            throw new Exception("Client already exists with the same name.");
                        }

                        obj.Id = clientId;

                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create client.", ex);
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
                    parameters.Add("@ClientId", id);
                    parameters.Add("@UpdatedBy", 1);
                    parameters.Add("@UpdatedAt", DateTime.Now);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync("DeleteClient", parameters, commandType: CommandType.StoredProcedure);

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete client.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_ClientMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int? SearchColumn)
        {
            throw new NotImplementedException();
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_ClientMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, long CompanyId , int? SearchColumn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SearchColumn", SearchColumn);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchTerm", searchTerm ?? "");
            parameters.Add("CompanyId", CompanyId);
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetClient", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var clients = (await multi.ReadAsync<tbl_ClientMaster>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filterRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filterRecords
                        };

                        return (paginationResponse, clients);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get clients.", ex);
            }
        }

        public async Task<tbl_ClientMaster?> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var client = await connection.QuerySingleOrDefaultAsync<tbl_ClientMaster>("GetClientById", parameters, commandType: CommandType.StoredProcedure);

                    return client;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get client by ID.", ex);
            }
        }

       

        public async Task<bool> UpdateAsync(tbl_ClientMaster obj)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@ClientId", obj.Id);
                    parameters.Add("@CompanyId", obj.CompanyId);
                    parameters.Add("@Code", obj.Code);
                    parameters.Add("@Name", obj.Name);
                    parameters.Add("@UpdatedBy", obj.UpdatedBy);
                    parameters.Add("@UpdatedAt", obj.UpdatedAt);

                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync("UpdateClient", parameters, commandType: CommandType.StoredProcedure);

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update client.", ex);
            }
        }
    }
}
