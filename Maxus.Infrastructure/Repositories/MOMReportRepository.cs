using Dapper;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maxus.Domain.DTOs;
using System.ComponentModel.Design;

namespace Maxus.Infrastructure.Repositories
{
    public class MOMReportRepository : IMOMReportRepository
    {
        private readonly IConfiguration _configuration;

        public MOMReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tbl_MOMReport> CreateAsync(tbl_MOMReport obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Date", obj.Date);
            parameters.Add("@SiteId", obj.SiteId);
            parameters.Add("@ActionBy", obj.ActionBy);
            parameters.Add("@Status", obj.Status);
            parameters.Add("@Remarks", obj.Remark);
            parameters.Add("@ClosureDate", obj.CloserDate);
            parameters.Add("@IsDraft", obj.IsDraft);
            parameters.Add("@CreatedBy", obj.CreatedBy);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            
           



            var clientDetailsTable = new DataTable();
            clientDetailsTable.Columns.Add("RepresentativeName", typeof(string));
            clientDetailsTable.Columns.Add("Email", typeof(string));
            clientDetailsTable.Columns.Add("PhoneNo", typeof(string));

            if (obj.clientRepresentatives is not null)
            {
                foreach (var detail in obj.clientRepresentatives)
                {
                    clientDetailsTable.Rows.Add(
                        detail.RepresentativeName,
                        detail.Email,
                        detail.PhoneNo
                    );
                }
            }

            var ImagePath = new DataTable();
            ImagePath.Columns.Add("AttachmentPath", typeof(string));
            

            if (obj.Attachment is not null)
            {
                foreach (var detail in obj.Attachment)
                {
                    ImagePath.Rows.Add(
                        detail
                    );
                }
            }


            var CompanyDetailsTable = new DataTable();
            CompanyDetailsTable.Columns.Add("RepresentativeName", typeof(string));
            CompanyDetailsTable.Columns.Add("Email", typeof(string));
            CompanyDetailsTable.Columns.Add("PhoneNo", typeof(string));

            if (obj.CompanyRepresentatives is not null)
            {
                foreach (var detail in obj.CompanyRepresentatives)
                {
                    CompanyDetailsTable.Rows.Add(
                        detail.RepresentativeName,
                        detail.Email,
                        detail.PhoneNo
                    );
                }
            }

            var PointTable = new DataTable();
            PointTable.Columns.Add("Points", typeof(string));
           
            if (obj.Points is not null)
            {
                foreach (var detail in obj.Points)
                {
                    PointTable.Rows.Add(
                        detail.Points
                    );
                }
            }


            parameters.Add("@ClientDetails", clientDetailsTable.AsTableValuedParameter("dbo.MOMClientRepresentativeDetailsTableType"));
            parameters.Add("@Attachment", ImagePath.AsTableValuedParameter("dbo.ImagePath"));
            parameters.Add("@CompanyDetails", CompanyDetailsTable.AsTableValuedParameter("dbo.MOMCompanyRepresentativeDetailsTableType"));
            parameters.Add("@Points", PointTable.AsTableValuedParameter("dbo.MOM_PointTableType"));
            parameters.Add("@ActionablePoint", obj.ActionablePoint);
            parameters.Add("@MOMId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateMOMReport", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int MOMId = parameters.Get<int>("@MOMId");
                        int error = parameters.Get<int>("@Error");

                        if (error != 0)
                        {
                            throw new Exception("MOM already exists with the same name.");
                        }

                        obj.Id = MOMId;


                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("MOM is already created with the same name.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_MOMReport>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int CompanyId, DateTime? FromDate, DateTime? ToDate , Boolean? IsDraft , int? SearchColumn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchTerm", searchTerm);
            parameters.Add("SearchColumn", SearchColumn);
            parameters.Add("CompanyId", CompanyId);
            parameters.Add("IsDraft", IsDraft ?? null);
            parameters.Add("FromDate", FromDate ?? null);
            parameters.Add("ToDate", ToDate ?? null);
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetMOMReports", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var branches = (await multi.ReadAsync<tbl_MOMReport>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filterRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filterRecords
                        };

                        return (paginationResponse, branches);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("MOM report GetAll Error In Repository.", ex);
            }
        }

        public async Task<IEnumerable<tbl_MOMReport>> GetByCompanyIdAsync(int UserId, int Comapnyid)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", UserId);
            parameters.Add("@CompanyId", Comapnyid);


            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("GetMOMReportsByCompany", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var MOMReport = (await multi.ReadAsync<tbl_MOMReport>()).ToList();


                        return MOMReport;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ClientRepresentativeDetails By siteId Error In Repository.", ex);
            }
        }

        public async Task<tbl_MOMReport?> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ReportId", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("GetMOMReportById", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var report = await multi.ReadSingleOrDefaultAsync<tbl_MOMReport>();

                        if (report != null)
                        {
                            report.clientRepresentatives = (await multi.ReadAsync<tbl_ClientRepresentative>()).ToList();
                            report.CompanyRepresentatives = (await multi.ReadAsync<tbl_ClientRepresentative>()).ToList();
                            report.Points = (await multi.ReadAsync<tbl_MOMPoints>()).ToList();
                            report.AttchmentPath = (await multi.ReadAsync<tbl_attchment>()).ToList();
                        }

                        return report;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting MOM report by ID.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_MOMReport obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", obj.Id);
            parameters.Add("@SiteId", obj.SiteId);
            parameters.Add("@ActionBy", obj.ActionBy);
            parameters.Add("@Status", obj.Status);
            parameters.Add("@Remarks", obj.Remark);
            parameters.Add("@ClosureDate", obj.CloserDate);
            parameters.Add("@IsDraft", obj.IsDraft);
            parameters.Add("@UpdatedAt", obj.UpdatedAt);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@UpdatedBy", obj.UpdatedBy);

            var clientDetailsTable = new DataTable();
            clientDetailsTable.Columns.Add("RepresentativeName", typeof(string));
            clientDetailsTable.Columns.Add("Email", typeof(string));
            clientDetailsTable.Columns.Add("PhoneNo", typeof(string));

            if (obj.clientRepresentatives is not null)
            {
                foreach (var detail in obj.clientRepresentatives)
                {
                    clientDetailsTable.Rows.Add(
                        detail.RepresentativeName,
                        detail.Email,
                        detail.PhoneNo
                    );
                }
            }

            var ImagePath = new DataTable();
            ImagePath.Columns.Add("AttachmentPath", typeof(string));


            if (obj.Attachment is not null)
            {
                foreach (var detail in obj.Attachment)
                {
                    ImagePath.Rows.Add(
                        detail
                    );
                }
            }


            var CompanyDetailsTable = new DataTable();
            CompanyDetailsTable.Columns.Add("RepresentativeName", typeof(string));
            CompanyDetailsTable.Columns.Add("Email", typeof(string));
            CompanyDetailsTable.Columns.Add("PhoneNo", typeof(string));

            if (obj.CompanyRepresentatives is not null)
            {
                foreach (var detail in obj.CompanyRepresentatives)
                {
                    CompanyDetailsTable.Rows.Add(
                        detail.RepresentativeName,
                        detail.Email,
                        detail.PhoneNo
                    );
                }
            }

            var PointTable = new DataTable();
            PointTable.Columns.Add("Points", typeof(string));

            if (obj.Points is not null)
            {
                foreach (var detail in obj.Points)
                {
                    PointTable.Rows.Add(
                        detail.Points
                    );
                }
            }


            parameters.Add("@ClientDetails", clientDetailsTable.AsTableValuedParameter("dbo.MOMClientRepresentativeDetailsTableType"));
            parameters.Add("@Attachment", ImagePath.AsTableValuedParameter("dbo.ImagePath"));
            parameters.Add("@CompanyDetails", CompanyDetailsTable.AsTableValuedParameter("dbo.MOMCompanyRepresentativeDetailsTableType"));
            parameters.Add("@Points", PointTable.AsTableValuedParameter("dbo.MOM_PointTableType"));
            parameters.Add("@ActionablePoint", obj.ActionablePoint);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("UpdateMOMReport", parameters, commandType: CommandType.StoredProcedure))
                    {
                       
                        int error = parameters.Get<int>("@Error");

                        if (error == 1)
                        {
                            throw new Exception("MOM already exists with the same name.");
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("MOM is already created with the same name.", ex);
            }
        }
    }
}
