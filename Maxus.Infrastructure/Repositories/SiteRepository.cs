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
    public class SiteRepository : ISiteRepository
    {
        private readonly IConfiguration _configuration;

        public SiteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tbl_SiteMaster> CreateAsync(tbl_SiteMaster obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BranchId", obj.BranchId);
            parameters.Add("@ClientId", obj.ClidetId);
            parameters.Add("@Code", obj.Code);
            parameters.Add("@Name", obj.Name);
            parameters.Add("@Address", obj.Address);
            parameters.Add("@Latitude", obj.Latitude);
            parameters.Add("@Longitude", obj.Longitude);
            parameters.Add("@IndustrySegmentId", obj.IndustrySegmentId);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@CreatedBy", obj.CreatedBy);


            var clientDetailsTable = new DataTable();
            clientDetailsTable.Columns.Add("RepresentativeName", typeof(string));
            clientDetailsTable.Columns.Add("Designation", typeof(string));
            clientDetailsTable.Columns.Add("Email", typeof(string));
            clientDetailsTable.Columns.Add("EmailTo", typeof(string));
            clientDetailsTable.Columns.Add("EmailCC", typeof(string));
            clientDetailsTable.Columns.Add("PhoneNo", typeof(string));

            if (obj.ClientRepresentatives is not null)
            {
                foreach (var detail in obj.ClientRepresentatives)
                {
                    string email = string.IsNullOrEmpty(detail.Email) ? null : detail.Email;

                    clientDetailsTable.Rows.Add(
                        detail.RepresentativeName,
                        detail.Designation,
                        email,
                        detail.EmailTo,
                        detail.EmailCC,
                        detail.PhoneNo
                    );
                }
            }

            parameters.Add("@ClientDetails", clientDetailsTable.AsTableValuedParameter("dbo.CreateClientRepresentativeDetailsType"));
            parameters.Add("@SiteId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateSite", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int siteId = parameters.Get<int>("@SiteId");
                        int error = parameters.Get<int>("@Error");

                        if (error == 1)
                        {
                            throw new Exception("Site already exists with the same name , Code , Branch , Client , IndustrySegment.");
                        }

                        obj.Id = siteId;


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
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@SiteId", id);
                    parameters.Add("@UpdatedBy", 1);
                    parameters.Add("@UpdatedAt", DateTime.Now);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "DeleteSite",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Site Delete Error In Repository.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_SiteMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm , int? SearchColumn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SearchColumn", SearchColumn);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchTerm", searchTerm ?? "");
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetSite", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var sites = (await multi.ReadAsync<tbl_SiteMaster>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filteredRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filteredRecords
                        };

                        return (paginationResponse, sites);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Site GetAll Error In Repository.", ex);
            }
            throw new NotImplementedException();
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_SiteMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int? SearchColumn, long? CompanyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SearchColumn", SearchColumn);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchTerm", searchTerm ?? "");
            parameters.Add("ComapnyId", CompanyId ?? 0);
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetSite", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var sites = (await multi.ReadAsync<tbl_SiteMaster>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filteredRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filteredRecords
                        };

                        return (paginationResponse, sites);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Site GetAll Error In Repository.", ex);
            }
        }

        public async Task<IEnumerable<tbl_SiteMaster>> GetByCompanyAndClientIdAsync(int CompanyId, long ClientId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", CompanyId);
            parameters.Add("ClientId", ClientId);
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("GetSiteByCompanyAndClient", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var sites = (await multi.ReadAsync<tbl_SiteMaster>()).ToList();


                        return sites;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Site GetAll Error In Repository.", ex);
            }
        }

        public async Task<tbl_SiteMaster?> GetByIdAsync(int id)
        {
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id);

                try
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        await connection.OpenAsync();

                        var lookup = new Dictionary<long, tbl_SiteMaster>();

                        await connection.QueryAsync<tbl_SiteMaster, tbl_ClientRepresentativeDetails, tbl_SiteMaster>(
                            "GetSiteById",
                            (site, representative) =>
                            {
                                tbl_SiteMaster siteEntry;

                                if (!lookup.TryGetValue(site.Id, out siteEntry))
                                {
                                    siteEntry = site;
                                    siteEntry.ClientRepresentatives = new List<tbl_ClientRepresentativeDetails>();
                                    lookup.Add(siteEntry.Id, siteEntry);
                                }

                                if (representative != null)
                                {
                                    siteEntry.ClientRepresentatives.Add(representative);
                                }

                                return siteEntry;
                            },
                            parameters,
                            splitOn: "RepresentativeId",
                            commandType: CommandType.StoredProcedure);

                        return lookup.Values.FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Site GetById Error In Repository.", ex);
                }
            }
        }

        public async Task<IEnumerable<tbl_SiteMaster>?> GetByUserIdAsync(int UserId, int Comapnyid)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", UserId);
            parameters.Add("@CompanyID", Comapnyid);
           

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("GetSiteByUser", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var Site = (await multi.ReadAsync<tbl_SiteMaster>()).ToList();
                       

                        return Site;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Site By UserId Error In Repository.", ex);
            }
        }

        public async Task<IEnumerable<tbl_ClientRepresentativeDetails>> GetRepresentativeBySiteIdAsync(int SiteId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SiteId", SiteId);
           
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("GetRepresentativeDetailsBySiteId", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var ClientRepresentativeDetails = (await multi.ReadAsync<tbl_ClientRepresentativeDetails>()).ToList();


                        return ClientRepresentativeDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ClientRepresentativeDetails By siteId Error In Repository.", ex);
            }
        }

        public async Task<IEnumerable<tbl_SiteMaster>> GetSiteByCompany(int CompanyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CompanyId", CompanyId);
           

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetSiteByCompany", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var sites = (await multi.ReadAsync<tbl_SiteMaster>()).ToList();
                       

                        return sites;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Site GetAll Error In Repository.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_SiteMaster obj)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@SiteId", obj.Id);
                    parameters.Add("@BranchId", obj.BranchId);
                    parameters.Add("@ClidetId", obj.ClidetId);
                    parameters.Add("@Code", obj.Code);
                    parameters.Add("@Name", obj.Name);
                    parameters.Add("@Address", obj.Address);
                    parameters.Add("@Latitude", obj.Latitude);
                    parameters.Add("@Longitude", obj.Longitude);
                    parameters.Add("@IndustrySegmentId", obj.IndustrySegmentId);
                    parameters.Add("@UpdatedBy", obj.UpdatedBy);
                    parameters.Add("@UpdatedAt", obj.UpdatedAt);

                    var clientDetailsTable = new DataTable();
                    clientDetailsTable.Columns.Add("Id", typeof(long));
                    clientDetailsTable.Columns.Add("SiteId", typeof(long));
                    clientDetailsTable.Columns.Add("RepresentativeName", typeof(string));
                    clientDetailsTable.Columns.Add("Designation", typeof(string));
                    clientDetailsTable.Columns.Add("Email", typeof(string));
                    clientDetailsTable.Columns.Add("EmailTo", typeof(string));
                    clientDetailsTable.Columns.Add("EmailCC", typeof(string));
                    clientDetailsTable.Columns.Add("PhoneNo", typeof(string));

                    if (obj.ClientRepresentatives is not null)
                    {
                        foreach (var detail in obj.ClientRepresentatives)
                        {
                            clientDetailsTable.Rows.Add(
                                detail.RepresentativeId,
                                obj.Id,
                                detail.RepresentativeName,
                                detail.Designation,
                                detail.Email,
                                detail.EmailTo,
                                detail.EmailCC,
                                detail.PhoneNo
                            );
                        }
                    }

                    parameters.Add("@ClientRepresentatives", clientDetailsTable.AsTableValuedParameter("dbo.UpdateClientRepresentativeType"));
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "UpdateSite",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    var success = parameters.Get<bool>("@Success");

                    return success;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Site Update Error In Repository.", ex);
            }
        }
    }
}
