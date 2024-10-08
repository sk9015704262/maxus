using AutoMapper;
using Maxus.Application.DTOs.AttachmentLimits;
using Maxus.Application.DTOs.TrainingReport;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class AttachmentLimitsService : IAttachmentLimitsService
    {
        private readonly IAttachmentLimitRepository _attachmentLimitRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AttachmentLimitsService(IAttachmentLimitRepository attachmentLimitRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _attachmentLimitRepository = attachmentLimitRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<AttachmentLimitByIdResponse> CreateAsync(CreateAttachmentLimitRequest request)
        {
            try
            {
                var attachmentLimit = new tbl_AttachmentLimits
                {
                    AttachmentTypeId = request.ReportTypeId,
                    AttachmentType = request.AttachmentType,
                   
                    Compulsion = request.Compulsion,
                    LimitCount = request.LimitCount,
                    Ids = request.Id,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserId()
                };

                var createdAttachmentLimit = await _attachmentLimitRepository.CreateAsync(attachmentLimit);
                return _mapper.Map<AttachmentLimitByIdResponse>(createdAttachmentLimit);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<AttachmentLimitListResponse>)> GetAllAsync(AttachmentLimitListRequest obj)
        {
            try
            {
                var (paginationResponse, Attachment) = await _attachmentLimitRepository.GetAllAsync(obj.PageNumber, obj.PageSize, obj.SortBy, obj.SortDir, obj.SearchTerm , obj.SearchColumn);
                var mappeddata = _mapper.Map<IEnumerable<AttachmentLimitListResponse>>(Attachment);
                return (paginationResponse, mappeddata);
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Attachment Error In Service.", ex);
            }
        }

        public async Task<AttachmentLimitByIdResponse?> GetByIdAsync(GetAttachmentBySiteRequest request)
        {
            try
            {
                var Attachment = await _attachmentLimitRepository.GetByIdAsync(request.SiteId , request.ReportType);
                return _mapper.Map<AttachmentLimitByIdResponse>(Attachment);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting Attachment Limit by id in service.", ex);
            }
        }

        public async Task<AttachmentLimitByIdResponse> UpdateAsync(int id, UpdateAttachmentLimitRequest request)
        {
            try
            {
                var visitReport = new tbl_AttachmentLimits
                {
                    Id = id,
                    AttachmentTypeId = request.AttachmentTypeId,
                    ClientId = request.ClientId,
                    CompanyId = request.CompanyId,
                    Compulsion = request.Compulsion,
                    LimitCount = request.LimitCount,
                    SiteId = request.SiteId,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = CurrentUserId()
                };
               
                var success = await _attachmentLimitRepository.UpdateAsync(visitReport);
                if (success)
                {
                    return _mapper.Map<AttachmentLimitByIdResponse>(visitReport);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Update AttachmentLimit Error In Service.", ex);
            }
        }

        public async Task<AttachmentLimitByIdResponse?> GetById(int id)
        {
            try
            {
                var Attachment = await _attachmentLimitRepository.GetById(id);
                return _mapper.Map<AttachmentLimitByIdResponse>(Attachment);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting Attachment Limit by id in service.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _attachmentLimitRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Attachment Error In Service.", ex);
            }
        }
    }
}
