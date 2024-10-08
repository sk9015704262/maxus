using AutoMapper;
using Maxus.Application.DTOs.TrainingReport;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Maxus.Application.Services
{
    public class TraninigReportService : ITrainingReportService
    {
        private readonly IConfiguration _configuration;
        private string ImagePath;
        private readonly ITraningReportRepository _traningReportRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TraninigReportService(ITraningReportRepository traningReportRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            ImagePath = _configuration["ImageBasePath"];
            _traningReportRepository = traningReportRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<int> CreateAsync(CreateTrainingReportRequest request)
        {
            try
            {
                if (request.Id != 0)
                {
                    bool IsUpdated = await UpdateAsync(request.Id, request);
                    if (IsUpdated == true)
                    {
                        return 1;
                    }
                    return 0;

                }
                else
                {

                    var Traning = new tblTrainingReport
                    {
                        Topic = request.Topic,
                        CreatedBy = CurrentUserId(),
                        ClientRepresentative = request.ClientRepresentative,
                        CreatedAt = request.CreatedAt,
                        SiteId = request.SiteId,
                        Status = request.Status,
                        Duration = request.Duration,
                        TrainerName = request.TrainerName,
                        TrainerDesignation = request.TrainerDesignation,
                        Department = request.Department,
                        TrainerFeedback = request.TrainerFeedback,
                        ActionPlan = request.ActionPlan,
                        TrainingReportAttendance = request.TrainingReportAttendance,
                        IsDraft = request.IsDraft,
                        TrainerSignature = request.TrainerSignature,
                        EmployeeSignature = request.EmployeeSignature,
                        AdditionalTrainerSignature = request.AdditionalTrainerSignature,
                        ClientSignature = request.ClientSignature,
                        Attachment = request.Attachment
                    };

                    var createdTraning = await _traningReportRepository.CreateAsync(Traning);

                    if (createdTraning != null)
                    {
                        return 2;
                    }

                    return 0;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Create Traning Error In Service.", ex);
            }


        }

        public async Task<(FilterRecordsResponse, IEnumerable<TrainingListResponse>)> GetAllAsync(TrainingReportListRequest obj)
        {
            try
            {
                var (paginationResponse, trainings) = await _traningReportRepository.GetAllAsync(obj.PageNumber, obj.PageSize, obj.SortBy, obj.SortDir, obj.SearchTerm, obj.CompanyId, obj.FromDate, obj.ToDate, obj.IsDraft, obj.SearchColumn);
                var mappeddata = _mapper.Map<IEnumerable<TrainingListResponse>>(trainings);
                return (paginationResponse, mappeddata);
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Training Error In Service.", ex);
            }
        }

        public async Task<bool> UpdateAsync(int id, CreateTrainingReportRequest request)
        {
            try
            {
                var TraningReport = new tblTrainingReport
                {
                    Id = id,
                    Topic = request.Topic,
                    UpdatedBy = CurrentUserId(),
                    ClientRepresentative = request.ClientRepresentative,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = request.CreatedAt,
                    SiteId = request.SiteId,
                    Status = request.Status,
                    Duration = request.Duration,
                    TrainerName = request.TrainerName,
                    TrainerDesignation = request.TrainerDesignation,
                    Department = request.Department,
                    TrainerFeedback = request.TrainerFeedback,
                    ActionPlan = request.ActionPlan,
                    TrainingReportAttendance = request.TrainingReportAttendance,
                    IsDraft = request.IsDraft,
                    TrainerSignature = request.TrainerSignature,
                    EmployeeSignature = request.EmployeeSignature,
                    AdditionalTrainerSignature = request.AdditionalTrainerSignature,
                    ClientSignature = request.ClientSignature,
                    Attachment = request.Attachment

                };

                var success = await _traningReportRepository.UpdateAsync(TraningReport);
                if (success)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Update Traning Error In Service.", ex);
            }
        }

        public async Task<TrainingReportByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var TrainingReport = await _traningReportRepository.GetByIdAsync(id);
                if (TrainingReport != null)
                {
                    TrainingReport.TrainerSignature = new Uri(new Uri(ImagePath), TrainingReport.TrainerSignature).ToString();
                    TrainingReport.EmployeeSignature = new Uri(new Uri(ImagePath), TrainingReport.EmployeeSignature).ToString();
                    TrainingReport.AdditionalTrainerSignature = new Uri(new Uri(ImagePath), TrainingReport.AdditionalTrainerSignature).ToString();
                    TrainingReport.ClientSignature = new Uri(new Uri(ImagePath), TrainingReport.ClientSignature).ToString();

                    if (TrainingReport.AttchmentPath != null)
                    {


                        foreach (var item in TrainingReport.AttchmentPath)
                        {
                            if (item.AttachmentPath != null)
                            {
                                item.AttachmentPath = new Uri(new Uri(ImagePath), item.AttachmentPath).ToString();

                            }
                        }


                    }
                }

                var Training = _mapper.Map<TrainingReportByIdResponse>(TrainingReport);
                return Training;
            }
            catch (Exception ex)
            {
                throw new Exception("Get Traning Report Error In Service.", ex);
            }
        }
    }
}
