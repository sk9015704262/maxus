using AutoMapper;
using Maxus.Application.DTOs.MOM;
using Maxus.Application.DTOs.VisitReport;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Maxus.Application.Services
{
    public class VisitReportService : IVisitReportService
    {
        private readonly IConfiguration _configuration;
        private string ImagePath;
        private readonly IVisitReportRepository _visitReportRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public VisitReportService(IVisitReportRepository visitReportRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor  , IConfiguration configuration)
        {
            _configuration = configuration;
            ImagePath = _configuration["ImageBasePath"];
            _visitReportRepository = visitReportRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<int> CreateAsync(CreateVisitReportRequest obj)
        {
            try
            {
                if (obj.Id != 0)
                {
                    bool IsUpdated = await UpdateAsync(obj.Id, obj);
                    if (IsUpdated == true)
                    {
                        return 1;
                    }
                    return 0;
                }
                else
                {
                    var VisitReport = new tbl_VisitReport
                    {
                        SiteId = obj.SiteId,
                        SiteSupervisorName = obj.SiteSupervisorName,
                        Remarks = obj.Remarks,
                        VisitCheckList = obj.VisitCheckList,
                        CreatedBy = CurrentUserId(),
                        CreatedAt = obj.CreatedAt,
                        Date = DateTime.Now,
                        IsDraft = obj.IsDraft,
                        Status = obj.Status,
                        ClientSignature = obj.ClientSignature,
                        ManagerSignature = obj.ManagerSignature,
                        Attachment = obj.Attachment

                    };

                    var createdVisitReport = await _visitReportRepository.CreateAsync(VisitReport);
                    if (createdVisitReport != null)
                    {
                        return 2;
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Create created VisitReport Error In Service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<VisitReportByListResponse>)> GetAllAsync(VisitReportListRequest request)
        {
            try
            {
                var (paginationResponse, VisitRepoet) = await _visitReportRepository.GetAllAsync(request.PageNumber, request.PageSize, request.SortBy, request.SortDir, request.SearchTerm, request.CompanyId, request.FromDate, request.ToDate , request.IsDraft , request.SearchColumn);
                return (paginationResponse, _mapper.Map<IEnumerable<VisitReportByListResponse>>(VisitRepoet));
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all Visit Report in service.", ex);
            }
        }

        public async Task<VisitReportByidResponse?> GetByIdAsync(int id)
        {
            try
            {
                var Report = await _visitReportRepository.GetByIdAsync(id);
                if (Report != null)
                {
                    Report.ClientSignature = new Uri(new Uri(ImagePath), Report.ClientSignature).ToString();
                    Report.ManagerSignature = new Uri(new Uri(ImagePath), Report.ManagerSignature).ToString();

                    if (Report.AttchmentPath != null)
                    {


                        foreach (var item in Report.AttchmentPath)
                        {
                            if (item.AttachmentPath != null)
                            {
                                item.AttachmentPath = new Uri(new Uri(ImagePath), item.AttachmentPath).ToString();

                            }
                        }


                    }
                }
                var VisitReport = _mapper.Map<VisitReportByidResponse>(Report);
                return VisitReport;
            }
            catch (Exception ex)
            {
                throw new Exception("Get Report Error In Service.", ex);
            }
        }

        public async Task<bool> UpdateAsync(int id, CreateVisitReportRequest obj)
        {
            try
            {
                var VisitReport = new tbl_VisitReport
                {
                    SiteId = obj.SiteId,
                    SiteSupervisorName = obj.SiteSupervisorName,
                    Remarks = obj.Remarks,
                    VisitCheckList = obj.VisitCheckList,
                    Id = id,
                    UpdatedBy = CurrentUserId(),
                    CreatedAt = obj.CreatedAt,
                    UpdatedAt = DateTime.Now,
                    Date = DateTime.Now,
                    IsDraft = obj.IsDraft,
                    Status = obj.Status,
                    ClientSignature = obj.ClientSignature,
                    ManagerSignature = obj.ManagerSignature,
                    Attachment = obj.Attachment

                };



                var createdVisitReport = await _visitReportRepository.UpdateAsync(VisitReport);
                if (createdVisitReport == false) {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Create createdVisitReport Error In Service.", ex);
            }
        }
    }
}
