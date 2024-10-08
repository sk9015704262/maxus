using AutoMapper;
using Maxus.Application.DTOs.VisitReport;
using Maxus.Application.DTOs.VisitReportChekList;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class VisitReportChekListService : IVisitReportChekListService
    {
        private readonly IVisitReportChekListRepository _visitReportRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public VisitReportChekListService(IVisitReportChekListRepository visitReportRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this._visitReportRepository = visitReportRepository;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<VisitReportByIdResponse> CreateAsync(CreateVisitReportCheckListRequest request)
        {
            try
            {
                var visitReport = new tbl_VisitReportChekListMaster
                {
                    Description = request.Description,
                    Name = request.Name,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserId(),
                    IndustrySegmentId = request.IndustrySegmentId,
                    CompanyId = request.CompanyId,
                    IsMandatory = request.IsMandatory,
                    ChekListOption = request.ChekListOption
                };

                var createdVisitReport = await _visitReportRepository.CreateAsync(visitReport);
                return _mapper.Map<VisitReportByIdResponse>(createdVisitReport);
            }
            catch (Exception ex)
            {
                throw new Exception("Create Visit Report Error In Service.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _visitReportRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Visit Report Error In Service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<VisitReportListResponse>)> GetAllAsync(VisitReportChekListRequest request)
        {
            try
            {
                var (paginationResponse, visitReports) = await _visitReportRepository.GetAllAsync(request.PageNumber, request.PageSize, request.SortBy, request.SortDir, request.SearchTerm , request.SearchColumn);
                var mappedVisitReports = _mapper.Map<IEnumerable<VisitReportListResponse>>(visitReports);
                return (paginationResponse, mappedVisitReports);
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Visit Reports Error In Service.", ex);
            }
        }

        public async Task<VisitReportByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var visitReport = await _visitReportRepository.GetByIdAsync(id);
                return _mapper.Map<VisitReportByIdResponse>(visitReport);
            }
            catch (Exception ex)
            {
                throw new Exception("GetById Visit Report Error In Service.", ex);
            }
        }

        public async Task<IEnumerable<VisitReportByCompanyListResponse>?> GetVisitByCompanyIdAsync(GetVisitByCompanyRequest request , int UserId)
        {
            try
            {
                var  visitReports = await _visitReportRepository.GetVisitByComanyIdAsync(UserId , request.CompanyId);
                return _mapper.Map<IEnumerable<VisitReportByCompanyListResponse>>(visitReports);
                
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Visit Reports Error In Service.", ex);
            }
        }

        public async Task<VisitReportByIdResponse> UpdateAsync(int id, UpdateVisitReportChekListRequest request)
        {
            try
            {
                var visitReport = new tbl_VisitReportChekListMaster
                {
                    Id = id,
                    Description = request.Description,
                    Name = request.Name,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = CurrentUserId(),
                    IndustrySegmentId = request.IndustrySegmentId,
                    CompanyId = request.CompanyId,
                    IsMandatory = request.IsMandatory,
                    ChekListOption = request.ChekListOption
                };

                    var success = await _visitReportRepository.UpdateAsync(visitReport);
                    if (success)
                    {
                        return _mapper.Map<VisitReportByIdResponse>(visitReport);
                    }
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Update Visit Report Error In Service.", ex);
            }
        }
    }
}
