using Dapper;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Maxus.Domain.DTOs;
using System.Net.Mail;

namespace Maxus.Infrastructure.Repositories
{
    public class AttachmentLimitsRepository : IAttachmentLimitRepository
    {
        private readonly IConfiguration _configuration;

        public AttachmentLimitsRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<tbl_AttachmentLimits> CreateAsync(tbl_AttachmentLimits attachmentLimits)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AttachmentType", attachmentLimits.AttachmentType);
            parameters.Add("@ReportType", attachmentLimits.AttachmentTypeId);
            parameters.Add("@Compulsion", attachmentLimits.Compulsion);
            parameters.Add("@LimitCount", attachmentLimits.LimitCount);
            parameters.Add("@CreatedBy", attachmentLimits.CreatedBy);
            parameters.Add("@CreatedAt", attachmentLimits.CreatedAt);

            var TrainingReportTopicTable = new DataTable();
            TrainingReportTopicTable.Columns.Add("ID", typeof(long));

            if (attachmentLimits.Ids is not null)
            {
                foreach (var detail in attachmentLimits.Ids)
                {
                    TrainingReportTopicTable.Rows.Add(
                        detail.Id
                    );
                }
            }
            parameters.Add("@Ids", TrainingReportTopicTable.AsTableValuedParameter("dbo.IDArray"));
            parameters.Add("@AttachmentId", dbType: DbType.Int32, direction: ParameterDirection.Output);  // Output parameter
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);  // Output parameter

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateAttachmentLimit", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int attachmentId = parameters.Get<int>("@AttachmentId");
                        int error = parameters.Get<int>("@Error");

                        if (error != 0 && attachmentId == 0)
                        {
                            throw new Exception($"Attachment already exists with the same name id {error}.");
                        }

                        attachmentLimits.Id = attachmentId;

                        return attachmentLimits;
                    }
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
                    parameters.Add("@Id", id);
                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "DeleteAttachmentById",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return parameters.Get<bool>("@Success");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Attachment Delete Error In Repository.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tbl_AttachmentLimits>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm , int? SearchColumn)
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

                    using (var multi = await connection.QueryMultipleAsync("sp_GetAttachmentLimits", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var Attachmentes = (await multi.ReadAsync<tbl_AttachmentLimits>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filterRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filterRecords
                        };

                        foreach (var Attachment in Attachmentes)
                        {
                            if (Attachment.AttachmentTypeId == 1)
                            {
                                Attachment.AttachmentType = "MOM Report";
                            }

                            if (Attachment.AttachmentTypeId == 2)
                            {
                                Attachment.AttachmentType = "Visit Report";
                            }

                            if (Attachment.AttachmentTypeId == 3)
                            {
                                Attachment.AttachmentType = "Training Report";
                            }

                            if (Attachment.AttachmentTypeId == 4)
                            {
                                Attachment.AttachmentType = "Customer Feedback Report";
                            }

                        }

                        foreach (var Attachment in Attachmentes)
                        {

                            if (Attachment.CompanyId != 0 && Attachment.ClientId == 0 && Attachment.SiteId == 0)
                            {
                                Attachment.Right = "Company";
                            }
                            else if (Attachment.CompanyId == 0 && Attachment.ClientId != 0 && Attachment.SiteId == 0)
                            {
                                Attachment.Right = "Client";
                            }
                            else if (Attachment.CompanyId == 0 && Attachment.ClientId == 0 && Attachment.SiteId != 0)
                            {
                                Attachment.Right = "Site";
                            }

                        }



                        return (paginationResponse, Attachmentes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("MOM report GetAll Error In Repository.", ex);
            }
        }

        public async Task<tbl_AttachmentLimits?> GetById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var Attachment = await connection.QuerySingleOrDefaultAsync<tbl_AttachmentLimits>(
                        "[GetAttachmentLimitById]",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    if (Attachment.AttachmentTypeId == 1)
                    {
                        Attachment.AttachmentType = "MOM Report";
                    }

                    if (Attachment.AttachmentTypeId == 2)
                    {
                        Attachment.AttachmentType = "Visit Report";
                    }

                    if (Attachment.AttachmentTypeId == 3)
                    {
                        Attachment.AttachmentType = "Training Report";
                    }

                    if (Attachment.AttachmentTypeId == 4)
                    {
                        Attachment.AttachmentType = "Customer Feedback Report";
                    }


                    if (Attachment.CompanyId != 0 && Attachment.ClientId == 0 && Attachment.SiteId == 0)
                    {
                        Attachment.Right = "Company";
                    }
                    else if (Attachment.CompanyId == 0 && Attachment.ClientId != 0 && Attachment.SiteId == 0)
                    {
                        Attachment.Right = "Client";
                    }
                    else if (Attachment.CompanyId == 0 && Attachment.ClientId == 0 && Attachment.SiteId != 0)
                    {
                        Attachment.Right = "Site";
                    }

                    return Attachment;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Attachment GetBy Id Error In Repository.", ex);
            }
        }

        public async Task<tbl_AttachmentLimits?> GetByIdAsync(int id , int SiteId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SiteId", id);
            parameters.Add("@ReportType", SiteId);



            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var Attachment = await connection.QuerySingleOrDefaultAsync<tbl_AttachmentLimits>(
                        "GetAttachmentBySiteId",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    if (Attachment != null)
                    {
                        if (Attachment.AttachmentTypeId == 1)
                        {
                            Attachment.AttachmentType = "MOM Report";
                        }

                        if (Attachment.AttachmentTypeId == 2)
                        {
                            Attachment.AttachmentType = "Visit Report";
                        }

                        if (Attachment.AttachmentTypeId == 3)
                        {
                            Attachment.AttachmentType = "Training Report";
                        }

                        if (Attachment.AttachmentTypeId == 4)
                        {
                            Attachment.AttachmentType = "Customer Feedback Report";
                        }



                        if (Attachment.CompanyId != 0 && Attachment.ClientId == 0 && Attachment.SiteId == 0)
                        {
                            Attachment.Right = "Company";
                        }
                        else if (Attachment.CompanyId == 0 && Attachment.ClientId != 0 && Attachment.SiteId == 0)
                        {
                            Attachment.Right = "Client";
                        }
                        else if (Attachment.CompanyId == 0 && Attachment.ClientId == 0 && Attachment.SiteId != 0)
                        {
                            Attachment.Right = "Site";
                        }
                    }


                    



                    return Attachment;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Attachment GetBy Id Error In Repository.", ex);
            }
        }

        public async Task<bool> UpdateAsync(tbl_AttachmentLimits obj)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", obj.Id);
                    parameters.Add("@CompanyId", obj.CompanyId == 0 ? null : obj.CompanyId);
                    parameters.Add("@ClientId", obj.ClientId == 0 ? null : obj.ClientId);
                    parameters.Add("@SiteId", obj.SiteId == 0 ? null : obj.SiteId);
                    parameters.Add("@AttachmentTypeId", obj.AttachmentTypeId);
                    parameters.Add("@Compulsion", obj.Compulsion);
                    parameters.Add("@LimitCount", obj.LimitCount);
                    parameters.Add("@UpdatedBy", obj.UpdatedBy);

                    parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(
                        "UpdateAttachmentLimit",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return parameters.Get<bool>("@Success");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("AttachmentLimit Update Error In Repository.", ex);
            }
        }
    }


}
