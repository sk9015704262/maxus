using AccountingAPI.Responses;
using Maxus.Application.DTOs.Site;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class SiteController : ControllerBase
    {
        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSite(CreateSiteRequest request)
        {
            try
            {
                var site = await _siteService.CreateAsync(request);
                if (site == null)
                {
                    return BadRequest($"Site is already created with the same name");
                }

                return Ok(new ApiResponse<SiteByIdResponse>(site, "Site created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAllSite(SiteListRequest request)
        {
            try
            {
                var (paginationResponse, Site) = await _siteService.GetAllAsync(request);
                return Ok(new ApiResponse<object>(new { Site = Site, paginationResponse.TotalRecords, paginationResponse.FilteredRecords }, "Sites fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetSite(GetSiteRequest request)
        {
            try
            {
                var site = await _siteService.GetByIdAsync(request.Id);
                if (site == null)
                {
                    return NotFound($"Site with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<SiteByIdResponse>(site, "Site fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSite(UpdateSiteRequest request)
        {
            try
            {
                var site = await _siteService.UpdateAsync(request.Id, request);
                if (site == null)
                {
                    return NotFound($"Site with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<SiteByIdResponse>(site, "Site updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSite(DeleteSiteRequest request)
        {
            try
            {
                var site = await _siteService.DeleteAsync(request.Id);
                if (site == null)
                {
                    return NotFound($"Site with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<object>(site, "Site deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }


        [HttpPost]

        public async Task<IActionResult> GetSiteByUser(GetSiteByUserRequest request)
        {
            try
            {
                var site = await _siteService.GetByUserIdAsync(request);
                if (site == null)
                {
                    return NotFound($"Site with ID {request.UserId} not found.");
                }

                return Ok(new ApiResponse<object>(site, "Site fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }


        }

        [HttpPost]

        public async Task<IActionResult> GetRepresentativeBysite(GetRepresentativeBySiteRequest request)
        {
            try
            {
                var RepresentativeDetails = await _siteService.GetRepresentativeBySiteIdAsync(request);
                if (RepresentativeDetails == null)
                {
                    return NotFound($"Site with ID {request} not found.");
                }

                return Ok(new ApiResponse<object>(RepresentativeDetails, "RepresentativeDetails fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }


        }

        //get site by comapny and client id

        [HttpPost]
        public async Task<IActionResult> GetSiteByCompanyAndClient(GetSiteByCompanyAndClientRequest request)
        {
            try
            {
                var site = await _siteService.GetByCompanyAndClientIdAsync(request);
                if (site == null)
                {
                    return NotFound($"Site with ID {request.CompanyId} not found.");
                }

                return Ok(new ApiResponse<object>(site, "Site fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetSiteByCompany(GetSiteByCompanyRequest request)
        {
            try
            {
                var site = await _siteService.GetSiteByCompany(request);
                if (site == null)
                {
                    return NotFound($"Site with ID {request.CompanyId} not found.");
                }

                return Ok(new ApiResponse<object>(site, "Site fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }

}
