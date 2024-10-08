using AutoMapper;
using Maxus.Application.DTOs.Branch;
using Maxus.Application.DTOs.MOM;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Maxus.Application.Services
{
    public class MOMReportService : IMOMReportService
    {
        private readonly IConfiguration _configuration;
        private string ImagePath;
        private readonly IMOMReportRepository _MOMReportRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MOMReportService(IMOMReportRepository MOMReportRepository , IMapper mapper , IHttpContextAccessor httpContextAccessor ,  IConfiguration configuration)
        {
            _configuration = configuration;
            ImagePath = _configuration["ImageBasePath"];
            _MOMReportRepository = MOMReportRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<int> CreateAsync(CreateMOMRequest obj)
        {
            try
            {
                if (obj.Id != 0)
                {
                    bool isUpdated = await UpdateAsync(obj.Id, obj);
                    if (isUpdated == true)
                    {
                        return 1;
                    }
                    return 0;
                }

                else
                {

                    var MOM = new tbl_MOMReport
                    {
                        SiteId = obj.SiteId,
                        ActionBy = obj.ActionBy,
                        Status = obj.Status,
                        Remark = obj.Remark,
                        CloserDate = obj.CloserDate,
                        clientRepresentatives = obj.clientRepresentatives,
                        CompanyRepresentatives = obj.CompanyRepresentatives,
                        Points = obj.Points,
                        Date = DateTime.Now,
                        IsDraft = obj.IsDraft,
                        CreatedAt = obj.CreatedAt,
                        CreatedBy = CurrentUserId(),
                        ActionablePoint = obj.ActionablePoint,
                        Attachment = obj.Attachment
                    };


                    var createdMOM = await _MOMReportRepository.CreateAsync(MOM);
                    if (createdMOM != null)
                    {
                        return 2;
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Create MOM Error In Service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<MOMReportListResponse>)> GetAllAsync(GetMOMReportRequest request)
        {
            try
            {
                var (paginationResponse, MOMRepotes) = await _MOMReportRepository.GetAllAsync(request.PageNumber, request.PageSize, request.SortBy, request.SortDir, request.SearchTerm , request.CompanyId,  request.FromDate   , request.ToDate , request.IsDraft , request.SearchColumn);
                return (paginationResponse, _mapper.Map<IEnumerable<MOMReportListResponse>>(MOMRepotes));
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all MOMRepotes in service.", ex);
            }
        }

        public async Task<IEnumerable<GetMOMByIdResponse>?> GetMOMReportByCompanyAsync(GetMOMReportByCompanyIdRequest request)
        {
            try
            {
                var MOM = await _MOMReportRepository.GetByCompanyIdAsync(request.UserId, request.CompanyId);
                return _mapper.Map<IEnumerable<GetMOMByIdResponse>>(MOM);
            }
            catch (Exception ex)
            {
                throw new Exception("Get MOM RepresentativeDetails Error In Service.", ex);
            }
        }

        public async Task<GetMOMByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var MOM = await _MOMReportRepository.GetByIdAsync(id);

                if (MOM.AttchmentPath != null)
                {
                   

                    foreach (var item in MOM.AttchmentPath)
                    {
                        if (item.AttachmentPath != null)
                        {
                            item.AttachmentPath = new Uri(new Uri(ImagePath), item.AttachmentPath).ToString();
                            
                        }
                    }

                    
                }
                return _mapper.Map<GetMOMByIdResponse>(MOM);
            }
            catch (Exception ex)
            {
                throw new Exception("Get MOM  Error In Service.", ex);
            }
        }

        public async Task<bool> UpdateAsync(int id, CreateMOMRequest obj)
        {
            try
            {
                var MOM = new tbl_MOMReport
                {
                    SiteId = obj.SiteId,
                    Id = id,
                    ActionBy = obj.ActionBy,
                    Status = obj.Status,
                    Remark = obj.Remark,
                    CloserDate = obj.CloserDate,
                    clientRepresentatives = obj.clientRepresentatives,
                    CompanyRepresentatives = obj.CompanyRepresentatives,
                    Points = obj.Points,
                    Date = DateTime.Now,
                    IsDraft = obj.IsDraft,
                    CreatedAt = obj.CreatedAt,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = CurrentUserId(),
                    ActionablePoint = obj.ActionablePoint,
                    Attachment = obj.Attachment
                };


                var createdMOM = await _MOMReportRepository.UpdateAsync(MOM);
                if (createdMOM == true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Create MOM Error In Service.", ex);
            }
        }
    }
}
