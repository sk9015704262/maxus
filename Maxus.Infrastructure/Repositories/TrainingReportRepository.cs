using Dapper;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Policy;
using Maxus.Domain.DTOs;
using System.Globalization;

namespace Maxus.Infrastructure.Repositories
{
    public class TrainingReportRepository : ITraningReportRepository
    {
        private readonly IConfiguration _configuration;

        public TrainingReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<tblTrainingReport> CreateAsync(tblTrainingReport obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SiteId", obj.SiteId);
            parameters.Add("@Duration", obj.Duration);
            parameters.Add("@TrainerName", obj.TrainerName);
            parameters.Add("@TrainerDesignation", obj.TrainerDesignation);
            parameters.Add("@Department", obj.Department);
            parameters.Add("@TrainerFeedback", obj.TrainerFeedback);
            parameters.Add("@ActionPlan ", obj.ActionPlan);
            parameters.Add("@IsDraft", obj.IsDraft);
            parameters.Add("@Status", obj.Status);
            parameters.Add("@TrainerSignature", obj.TrainerSignature);
            parameters.Add("@EmployeeSignature", obj.EmployeeSignature);
            parameters.Add("@AdditionalTrainerSignature", obj.AdditionalTrainerSignature);
            parameters.Add("@ClientSignature", obj.ClientSignature);


            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@CreatedBy", obj.CreatedBy);

            var TrainingReportAttendanceTable = new DataTable();
            TrainingReportAttendanceTable.Columns.Add("EmployeeId", typeof(string));
            TrainingReportAttendanceTable.Columns.Add("EmployeeName", typeof(string));
            TrainingReportAttendanceTable.Columns.Add("EmployeeDesignation", typeof(string));

            if (obj.TrainingReportAttendance is not null)
            {
                foreach (var detail in obj.TrainingReportAttendance)
                {
                    TrainingReportAttendanceTable.Rows.Add(
                        detail.EmployeeId,
                        detail.EmployeeName,
                        detail.EmployeeDesignation
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

            var TrainingReportTopicTable = new DataTable();
            TrainingReportTopicTable.Columns.Add("TopicId", typeof(long));

            if (obj.Topic is not null)
            {
                foreach (var detail in obj.Topic)
                {
                    TrainingReportTopicTable.Rows.Add(
                        detail.TopicId
                    );
                }
            }

            var TrainingClientRepresentativeType = new DataTable();
            TrainingClientRepresentativeType.Columns.Add("RepresentativeName", typeof(string));
            TrainingClientRepresentativeType.Columns.Add("Email", typeof(string));
            TrainingClientRepresentativeType.Columns.Add("PhoneNo", typeof(string));


            if (obj.ClientRepresentative is not null)
            {
                foreach (var detail in obj.ClientRepresentative)
                {
                    TrainingClientRepresentativeType.Rows.Add(
                        detail.RepresentativeName,
                        detail.Email,
                        detail.PhoneNo
                    );
                }
            }

            parameters.Add("@TopicId", TrainingReportTopicTable.AsTableValuedParameter("dbo.TrainingReportTopicCreateType"));
            parameters.Add("@Attachment", ImagePath.AsTableValuedParameter("dbo.ImagePath"));
            parameters.Add("@ClientRepresentativeType", TrainingClientRepresentativeType.AsTableValuedParameter("dbo.TrainingClientRepresentativeType"));
            parameters.Add("@TrainingReportAttendance", TrainingReportAttendanceTable.AsTableValuedParameter("dbo.TrainingReportAttendanceCreateType"));
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);  // Output parameter
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);  // Output parameter

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("CreateTrainingReport", parameters, commandType: CommandType.StoredProcedure))
                    {
                        int Id = parameters.Get<int>("@Id");
                        int error = parameters.Get<int>("@Error");

                        if (error != 0)
                        {
                            throw new Exception("Traning Report already exists with the same name.");
                        }


                        obj.Id = Id;
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Traning Report is already created with the same name.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<tblTrainingReport>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int CompanyId, DateTime? FromDate, DateTime? ToDate , Boolean? IsDraft , int? SearchColumn)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            parameters.Add("SortBy", sortBy);
            parameters.Add("SortDir", sortDir);
            parameters.Add("SearchTerm", searchTerm ?? "");
            parameters.Add("SearchColumn", SearchColumn);
            parameters.Add("CompanyId", CompanyId );
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

                    using (var multi = await connection.QueryMultipleAsync("sp_GetTrainingReports", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var trainings = (await multi.ReadAsync<tblTrainingReport>()).ToList();
                        var totalRecords = parameters.Get<int>("TotalRecords");
                        var filteredRecords = parameters.Get<int>("FilterRecords");
                        var paginationResponse = new FilterRecordsResponse
                        {
                            TotalRecords = totalRecords,
                            FilteredRecords = filteredRecords
                        };

                        return (paginationResponse, trainings);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all Traning Report.", ex);
            }
        }

        public async Task<tblTrainingReport?> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ReportId", id);

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("getTrainingReportById", parameters, commandType: CommandType.StoredProcedure))
                    {
                        // Read the main report details
                        var report = await multi.ReadSingleOrDefaultAsync<tblTrainingReport>();

                        if (report != null)
                        {
                            // Read attendance details
                            report.TrainingReportAttendance = (await multi.ReadAsync<TrainingReportAttendance>()).ToList();

                            // Read topics
                            report.TopicId = (await multi.ReadAsync<TrainingReportTopics>()).ToList();

                            // Read client representatives
                            report.ClientRepresentative = (await multi.ReadAsync<tbl_ClientRepresentative>()).ToList();

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

        public async Task<bool> UpdateAsync(tblTrainingReport obj)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@SiteId", obj.SiteId);
            parameters.Add("@Id", obj.Id);
            parameters.Add("@Duration", obj.Duration);
            parameters.Add("@TrainerName", obj.TrainerName);
            parameters.Add("@TrainerDesignation", obj.TrainerDesignation);
            parameters.Add("@Department", obj.Department);
            parameters.Add("@TrainerFeedback", obj.TrainerFeedback);
            parameters.Add("@ActionPlan ", obj.ActionPlan);
            parameters.Add("@IsDraft", obj.IsDraft);
            parameters.Add("@Status", obj.Status);
            parameters.Add("@TrainerSignature", obj.TrainerSignature);
            parameters.Add("@EmployeeSignature", obj.EmployeeSignature);
            parameters.Add("@AdditionalTrainerSignature", obj.AdditionalTrainerSignature);
            parameters.Add("@ClientSignature", obj.ClientSignature);

            parameters.Add("@CreatedAt", obj.CreatedAt);
            parameters.Add("@UpdatedAt", obj.UpdatedAt);
            parameters.Add("@UpdatedBy", obj.UpdatedBy);

            var TrainingReportAttendanceTable = new DataTable();
            TrainingReportAttendanceTable.Columns.Add("EmployeeId", typeof(string));
            TrainingReportAttendanceTable.Columns.Add("EmployeeName", typeof(string));
            TrainingReportAttendanceTable.Columns.Add("EmployeeDesignation", typeof(string));

            if (obj.TrainingReportAttendance is not null)
            {
                foreach (var detail in obj.TrainingReportAttendance)
                {
                    TrainingReportAttendanceTable.Rows.Add(
                        detail.EmployeeId,
                        detail.EmployeeName,
                        detail.EmployeeDesignation
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

            var TrainingReportTopicTable = new DataTable();
            TrainingReportTopicTable.Columns.Add("TopicId", typeof(long));

            if (obj.Topic is not null)
            {
                foreach (var detail in obj.Topic)
                {
                    TrainingReportTopicTable.Rows.Add(
                        detail.TopicId
                    );
                }
            }

            var TrainingClientRepresentativeType = new DataTable();
            TrainingClientRepresentativeType.Columns.Add("RepresentativeName", typeof(string));
            TrainingClientRepresentativeType.Columns.Add("Email", typeof(string));
            TrainingClientRepresentativeType.Columns.Add("PhoneNo", typeof(string));


            if (obj.ClientRepresentative is not null)
            {
                foreach (var detail in obj.ClientRepresentative)
                {
                    TrainingClientRepresentativeType.Rows.Add(
                        detail.RepresentativeName,
                        detail.Email,
                        detail.PhoneNo
                    );
                }
            }

            parameters.Add("@TopicId", TrainingReportTopicTable.AsTableValuedParameter("dbo.TrainingReportTopicCreateType"));
            parameters.Add("@Attachment", ImagePath.AsTableValuedParameter("dbo.ImagePath"));
            parameters.Add("@ClientRepresentativeType", TrainingClientRepresentativeType.AsTableValuedParameter("dbo.TrainingClientRepresentativeType"));
            parameters.Add("@TrainingReportAttendance", TrainingReportAttendanceTable.AsTableValuedParameter("dbo.TrainingReportAttendanceCreateType"));
           
            parameters.Add("@Error", dbType: DbType.Int32, direction: ParameterDirection.Output);  // Output parameter

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var multi = await connection.QueryMultipleAsync("UpdateTraningReport", parameters, commandType: CommandType.StoredProcedure))
                    {
                       
                        int error = parameters.Get<int>("@Error");

                        if (error == 1)
                        {
                            throw new Exception("Traning Report already exists with the same name.");
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Traning Report is already created with the same name.", ex);
            }
        }
    }
}
