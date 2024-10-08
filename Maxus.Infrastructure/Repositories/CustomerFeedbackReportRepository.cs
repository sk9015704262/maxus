using Dapper;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities.PartialEntities;

namespace Maxus.Infrastructure.Repositories
{
    public class CustomerFeedbackReportRepository : ICustomerFeedbackReportRepository
    {
        private readonly IConfiguration _configuration;

        public CustomerFeedbackReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tbl_CustomerFeedbackReport> CreateAsync(tbl_CustomerFeedbackReport obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SiteId", obj.SiteId);
            parameters.Add("@Remark", obj.Remark);
            parameters.Add("@Date", obj.Date);
            parameters.Add("@IsDraft", obj.IsDraft);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@CreatedBy", obj.CreatedBy);
            parameters.Add("@Status", obj.Status);
            parameters.Add("@ClientSignature", obj.ClientSignature);
            parameters.Add("@ManagerSignature", obj.ManagerSignature);


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

            var ChackListTable = new DataTable();
            ChackListTable.Columns.Add("CustomerFeedbackId", typeof(long));
            ChackListTable.Columns.Add("IsSelected", typeof(Boolean));
            ChackListTable.Columns.Add("optionId", typeof(long));


            if (obj.FeedbackCheckList is not null)
            {
                foreach (var detail in obj.FeedbackCheckList)
                {
                    ChackListTable.Rows.Add(
                        detail.Id,
                        detail.IsSelected,
                        detail.OptionId
                    );
                }
            }

            parameters.Add("@ClientDetails", clientDetailsTable.AsTableValuedParameter("dbo.MOMClientRepresentativeDetailsTableType"));
            parameters.Add("@ChackList", ChackListTable.AsTableValuedParameter("dbo.FeedbackChecklist_TableType"));
            parameters.Add("@Attachment", ImagePath.AsTableValuedParameter("dbo.ImagePath"));
            parameters.Add("@FeedbackId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("InsertCustomerFeedbackReport", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int FeedbackId = parameters.Get<int>("@FeedbackId");
                        int error = parameters.Get<int>("@Error");

                        if (error != 0)
                        {
                            throw new Exception("Company already exists with the same name.");
                        }

                        obj.Id = FeedbackId;


                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Company is already created with the same name.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_CustomerFeedbackReport>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int CompanyId , DateTime? FromDate, DateTime? ToDate , Boolean? IsDraft , int? SearchColumn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchTerm", searchTerm);
            parameters.Add("CompanyId", CompanyId);
            parameters.Add("SearchColumn", SearchColumn);

            parameters.Add("FromDate", FromDate);
            parameters.Add("IsDraft", IsDraft ?? null);
            parameters.Add("ToDate", ToDate);
            parameters.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("FilterRecords", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("sp_GetFeedbackReports", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var CustomerFeedback = (await multi.ReadAsync<tbl_CustomerFeedbackReport>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filterRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filterRecords
                        };

                        return (paginationResponse, CustomerFeedback);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CustomerFeedback GetAll Error In Repository.", ex);
            }
        }

            public async Task<tbl_CustomerFeedbackReport?> GetByIdAsync(int id)
            {
                var parameters = new DynamicParameters();
                parameters.Add("ReportId", id);

           
                 try
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        await connection.OpenAsync();

                        using (var multi = await connection.QueryMultipleAsync("getFeedbackReportById", parameters, commandType: CommandType.StoredProcedure))
                        {
                            // Read the main report details
                            var report = await multi.ReadSingleOrDefaultAsync<tbl_CustomerFeedbackReport>();

                        if (report != null)
                        {
                            // Read the result sets in the correct order
                            var checklist = await multi.ReadAsync<tbl_CustomerFeedbackMaster>();
                            report.CheckLists = checklist.ToList();
                            var options = await multi.ReadAsync<tbl_OptionFeedbackReport>();
                            report.CheckListOptions = options.ToList();
                            var clientRepresentatives = await multi.ReadAsync<tbl_ClientRepresentative>();
                            report.clientRepresentatives = clientRepresentatives.ToList();
                            var attachments = await multi.ReadAsync<tbl_attchment>();
                            report.AttchmentPath = attachments.ToList();


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

        public async Task<IEnumerable<tbl_CustomerFeedbackReport?>> GetFeedbackByComanyIdAsync(int userId, int companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@CompanyId", companyId);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("GetCustomerFeedbackCheckListByCompany", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var FeedbackByComany = (await multi.ReadAsync<tbl_CustomerFeedbackReport>()).ToList();


                        return FeedbackByComany;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all visits.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_CustomerFeedbackReport obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SiteId", obj.SiteId);
            parameters.Add("@Remark", obj.Remark);
            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@IsDraft", obj.IsDraft);
            parameters.Add("@UpdatedAt", obj.UpdatedAt);
            parameters.Add("@UpdatedBy", obj.UpdatedBy);
            parameters.Add("@Id", obj.Id);
            parameters.Add("@Status", obj.Status);
            parameters.Add("@ClientSignature", obj.ClientSignature);
            parameters.Add("@ManagerSignature", obj.ManagerSignature);


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

            var ChackListTable = new DataTable();
            ChackListTable.Columns.Add("CustomerFeedbackId", typeof(long));
            ChackListTable.Columns.Add("IsSelected", typeof(Boolean));
            ChackListTable.Columns.Add("optionId", typeof(long));


            if (obj.FeedbackCheckList is not null)
            {
                foreach (var detail in obj.FeedbackCheckList)
                {
                    ChackListTable.Rows.Add(
                        detail.Id,
                        detail.IsSelected,
                        detail.OptionId
                    );
                }
            }

            parameters.Add("@ClientDetails", clientDetailsTable.AsTableValuedParameter("dbo.MOMClientRepresentativeDetailsTableType"));
            parameters.Add("@ChackList", ChackListTable.AsTableValuedParameter("dbo.FeedbackChecklist_TableType"));
            parameters.Add("@Attachment", ImagePath.AsTableValuedParameter("dbo.ImagePath"));
            
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("UpdateCustomerFeedbackReport", parameters, commandType: CommandType.StoredProcedure))
                    {
                        
                        int error = parameters.Get<int>("@Error");

                        if (error == 1)
                        {
                            throw new Exception("Company already exists with the same name.");
                        }

                        


                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Company is already created with the same name.", ex);
            }
        }
    }
}
