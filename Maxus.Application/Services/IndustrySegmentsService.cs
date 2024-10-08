using AutoMapper;
using Maxus.Application.DTOs.IndustrySegments;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class IndustrySegmentsService : IIndustrySegmentsService
    {
        private readonly IIndustrySegmentsRepository _industrySegmentsRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public IndustrySegmentsService(IIndustrySegmentsRepository industrySegmentsRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _industrySegmentsRepository = industrySegmentsRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<IndustrySegmentsByIdResponse> CreateAsync(CreateIndustrySegmentsRequest obj)
        {
            try
            {
                var industrySegments = new tbl_IndustrySegments
                {
                    Code = obj.Code,
                    Name = obj.Name,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserId()
                };

                var industrySegmentsFinal = await _industrySegmentsRepository.CreateAsync(industrySegments);
                return _mapper.Map<IndustrySegmentsByIdResponse>(industrySegmentsFinal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _industrySegmentsRepository.DeleteAsync(id);
        }

        public async Task<(FilterRecordsResponse, IEnumerable<IndustrySegmentsListResponse>)> GetAllAsync(IndustrySegmentsListRequest obj)
        {
            var (paginationResponse, industrySegments) = await _industrySegmentsRepository.GetAllAsync(obj.PageNumber, obj.PageSize, obj.SortBy, obj.SortDir, obj.SearchTerm , obj.SearchColumn);
            var industrySegmentsListResponse = _mapper.Map<IEnumerable<IndustrySegmentsListResponse>>(industrySegments);
            return (paginationResponse, industrySegmentsListResponse);
        }

        public async Task<IndustrySegmentsByIdResponse?> GetByIdAsync(int id)
        {
            var industrySegments = await _industrySegmentsRepository.GetByIdAsync(id);
            return _mapper.Map<IndustrySegmentsByIdResponse>(industrySegments);
        }

        public async Task<IndustrySegmentsByIdResponse> UpdateAsync(int id, UpdateIndustrySegmentsRequest request)
        {
            var industrySegments = await _industrySegmentsRepository.GetByIdAsync(id);
            if (industrySegments != null)
            {
                industrySegments.UpdatedAt = DateTime.Now;
                industrySegments.UpdatedBy = CurrentUserId();
                industrySegments.Name = request.Name;
                industrySegments.Code = request.Code;
                var success = await _industrySegmentsRepository.UpdateAsync(industrySegments);
                if (success)
                {
                    return _mapper.Map<IndustrySegmentsByIdResponse>(industrySegments);
                }
            }
            return null;
        }
    }
}
