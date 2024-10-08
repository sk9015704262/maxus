using AutoMapper;
using Maxus.Application.DTOs.Site;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public SiteService(ISiteRepository siteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _siteRepository = siteRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<SiteByIdResponse> CreateAsync(CreateSiteRequest obj)
        {
            try
            {
                var site = new tbl_SiteMaster
                {
                    BranchId = obj.BranchId,
                    ClidetId = obj.ClidetId,
                    Address = obj.Address,
                    Latitude = obj.Latitude,
                    Longitude = obj.Longitude,
                    IndustrySegmentId = obj.IndustrySegmentId,
                    Code = obj.Code,
                    Name = obj.Name,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserId()
                };

                if (obj.ClientRepresentatives is not null)
                {
                    site.ClientRepresentatives = obj.ClientRepresentatives;
                }

                var createdSite = await _siteRepository.CreateAsync(site);
                return _mapper.Map<SiteByIdResponse>(createdSite);
            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _siteRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Site Error In Service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<SiteListResponse>)> GetAllAsync(SiteListRequest obj)
        {
            try
            {
                var (paginationResponse, sites) = await _siteRepository.GetAllAsync(obj.PageNumber, obj.PageSize, obj.SortBy, obj.SortDir, obj.SearchTerm , obj.SearchColumn,obj.CompanyId);
                var siteListResponses = _mapper.Map<IEnumerable<SiteListResponse>>(sites);
                return (paginationResponse, siteListResponses);
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Site Error In Service.", ex);
            }
        }

        public async Task<SiteByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var site = await _siteRepository.GetByIdAsync(id);
                return _mapper.Map<SiteByIdResponse>(site);
            }
            catch (Exception ex)
            {
                throw new Exception("GetById Site Error In Service.", ex);
            }
        }

        public async Task<IEnumerable<GetSiteByUserResponse>?> GetByUserIdAsync(GetSiteByUserRequest getSiteByUserRequest)
        {
            try
            {
                var site = await _siteRepository.GetByUserIdAsync(getSiteByUserRequest.UserId , getSiteByUserRequest.ComapanyId);
                return _mapper.Map<IEnumerable<GetSiteByUserResponse>>(site);
            }
            catch (Exception ex)
            {
                throw new Exception("GetById Site Error In Service.", ex);
            }
        }

        public async Task<IEnumerable<ClientRepresentativeDetails>?> GetRepresentativeBySiteIdAsync(GetRepresentativeBySiteRequest request)
        {
            try
            {
                var RepresentativeDetails = await _siteRepository.GetRepresentativeBySiteIdAsync(request.SiteId);
                return _mapper.Map<IEnumerable<ClientRepresentativeDetails>>(RepresentativeDetails);
            }
            catch (Exception ex)
            {
                throw new Exception("Get SiteId RepresentativeDetails Error In Service.", ex);
            }
        }

        public async Task<SiteByIdResponse> UpdateAsync(int id, UpdateSiteRequest request)
        {
            try
            {
                var site = await _siteRepository.GetByIdAsync(id);
                if (site != null)
                {
                    site.BranchId = request.BranchId;
                    site.ClidetId = request.ClidetId;
                    site.IndustrySegmentId = request.IndustrySegmentId;
                    site.Address = request.Address;
                    site.Latitude = request.Latitude;
                    site.Longitude = request.Longitude;
                    site.UpdatedAt = DateTime.Now;
                    site.UpdatedBy = CurrentUserId();
                    site.Name = request.Name;
                    site.Code = request.Code;
                    if (request.ClientRepresentatives is not null)
                    {
                        site.ClientRepresentatives = request.ClientRepresentatives;
                    }
                    var success = await _siteRepository.UpdateAsync(site);
                    if (success)
                    {
                        return _mapper.Map<SiteByIdResponse>(site);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Update Site Error In Service.", ex);
            }
        }

        public async Task<IEnumerable<SiteListResponse>> GetByCompanyAndClientIdAsync(GetSiteByCompanyAndClientRequest obj)
        {
            try
            {
                var sites = await _siteRepository.GetByCompanyAndClientIdAsync(obj.CompanyId , obj.ClientId);
                var siteListResponses = _mapper.Map<IEnumerable<SiteListResponse>>(sites);
                return siteListResponses;
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Site Error In Service.", ex);
            }
        }

        public async Task<IEnumerable<SiteListResponse>?> GetSiteByCompany(GetSiteByCompanyRequest request)
        {
            try
            {
                var sites = await _siteRepository.GetSiteByCompany(request.CompanyId);
                var siteListResponses = _mapper.Map<IEnumerable<SiteListResponse>>(sites);
                return siteListResponses;
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll Site Error In Service.", ex);
            }
        }
    }
}
