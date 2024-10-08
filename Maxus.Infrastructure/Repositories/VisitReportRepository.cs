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
using Maxus.Domain.Entities.PartialEntities;

namespace Maxus.Infrastructure.Repositories
{
    public class VisitReportRepository : IVisitReportRepository
    {
        private readonly IConfiguration _configuration;

        public VisitReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tbl_VisitReport> CreateAsync(tbl_VisitReport obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SiteId", obj.SiteId);
            parameters.Add("@SiteSupervisorName", obj.SiteSupervisorName);
            parameters.Add("@Remarks", obj.Remarks);
           
            parameters.Add("@CreatedBy", obj.CreatedBy);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@Date", obj.Date);
            parameters.Add("@IsDraft", obj.IsDraft);
            parameters.Add("@Status", obj.Status);
            parameters.Add("@ClientSignature", obj.ClientSignature);
            parameters.Add("@ManagerSignature", obj.ManagerSignature);



            var ChackListTable = new DataTable();
            ChackListTable.Columns.Add("VisitChekListId", typeof(long));
            ChackListTable.Columns.Add("IsSelected", typeof(Boolean));
            ChackListTable.Columns.Add("ActionTaken", typeof(string));
            ChackListTable.Columns.Add("ClosureDate", typeof(DateTime));


            if (obj.VisitCheckList is not null)
            {
                foreach (var detail in obj.VisitCheckList)
                {
                    ChackListTable.Rows.Add(
                        detail.Id,
                        detail.IsSelected,
                        detail.ActionTaken,
                        detail.ClosureDate
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


            parameters.Add("@ChackList", ChackListTable.AsTableValuedParameter("[dbo].[VisitChekListType]"));
            parameters.Add("@Attachment", ImagePath.AsTableValuedParameter("dbo.ImagePath"));
            parameters.Add("@VisitReportId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateVisitReport", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int VisitReportId = parameters.Get<int>("@VisitReportId");
                        int error = parameters.Get<int>("@Error");

                        if (error != 0)
                        {
                            throw new Exception("Report already exists with the same name.");
                        }

                        obj.Id = VisitReportId;


                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Report is already created with the same name.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_VisitReport>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int CompanyId, DateTime? FromDate, DateTime? ToDate, Boolean? IsDraft , int? SearchColumn)
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
            parameters.Add("FromDate", FromDate);
            parameters.Add("ToDate", ToDate);
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetVisitReports", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var Visit = (await multi.ReadAsync<tbl_VisitReport>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filterRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filterRecords
                        };

                        return (paginationResponse, Visit);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Visit report GetAll Error In Repository.", ex);
            }
        }

        public async Task<tbl_VisitReport?> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ReportId", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("getVisitReportById", parameters, commandType: CommandType.StoredProcedure))
                    {
                        // Read the main report details
                        var report = await multi.ReadSingleOrDefaultAsync<tbl_VisitReport>();

                        if (report != null)
                        {
                            
                            report.VisitReportChecklist = (await multi.ReadAsync<tbl_OptionVisitReport>()).ToList();
                            report.AttchmentPath = (await multi.ReadAsync<tbl_attchment>()).ToList();
                        }

                        return report;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting Traning report by ID.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_VisitReport obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SiteId", obj.SiteId);
            parameters.Add("@SiteSupervisorName", obj.SiteSupervisorName);
            parameters.Add("@Remarks", obj.Remarks);
            parameters.Add("@Id", obj.Id);
            parameters.Add("@UpdatedBy", obj.UpdatedBy);
            parameters.Add("@UpdatedAt", obj.UpdatedAt);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@IsDraft", obj.IsDraft);
            parameters.Add("@Status", obj.Status);
            parameters.Add("@ClientSignature", obj.ClientSignature);
            parameters.Add("@ManagerSignature", obj.ManagerSignature);


            var ChackListTable = new DataTable();
            ChackListTable.Columns.Add("VisitChekListId", typeof(long));
            ChackListTable.Columns.Add("IsSelected", typeof(Boolean));
            ChackListTable.Columns.Add("ActionTaken", typeof(string));
            ChackListTable.Columns.Add("ClosureDate", typeof(DateTime));


            if (obj.VisitCheckList is not null)
            {
                foreach (var detail in obj.VisitCheckList)
                {
                    ChackListTable.Rows.Add(
                        detail.Id,
                        detail.IsSelected,
                        detail.ActionTaken,
                        detail.ClosureDate
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


            parameters.Add("@ChackList", ChackListTable.AsTableValuedParameter("[dbo].[VisitChekListType]"));
            parameters.Add("@Attachment", ImagePath.AsTableValuedParameter("dbo.ImagePath"));
          
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_UpdateVisitReport", parameters, commandType: CommandType.StoredProcedure))
                    {
                      
                        int error = parameters.Get<int>("@Error");

                        if (error == 1)
                        {
                            throw new Exception("Report already exists with the same name.");
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Report is already created with the same name.", ex);
            }
        }
    }
}
