using AccountingAPI.Responses;
using Maxus.Application.DTOs.IndustrySegments;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class IndustrySegmentsController : ControllerBase
    {
        private readonly IIndustrySegmentsService _industrySegmentsService;

        public IndustrySegmentsController(IIndustrySegmentsService industrySegmentsService)
        {
            _industrySegmentsService = industrySegmentsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIndustrySegments(CreateIndustrySegmentsRequest request)
        {
            
            try
            {
                var industrySegments = await _industrySegmentsService.CreateAsync(request);
                if (industrySegments == null)
                {
                    return BadRequest($"Industry segment is already created with the same name");
                }

                return Ok(new ApiResponse<IndustrySegmentsByIdResponse>(industrySegments, "IndustrySegments created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAllIndustrySegments(IndustrySegmentsListRequest request)
        {
            try
            {
                var (paginationResponse, industrySegments) = await _industrySegmentsService.GetAllAsync(request);
                return Ok(new ApiResponse<object>(new { industrySegments, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "IndustrySegments fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetIndustrySegments(GetIndustrySegmentsReqest request)
        {
            try
            {
                var industrySegments = await _industrySegmentsService.GetByIdAsync(request.Id);
                if (industrySegments == null)
                {
                    return NotFound($"IndustrySegments with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<IndustrySegmentsByIdResponse>(industrySegments, "IndustrySegments fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIndustrySegments(UpdateIndustrySegmentsRequest request)
        {
            try
            {
                var industrySegments = await _industrySegmentsService.UpdateAsync(request.Id, request);
                if (industrySegments == null)
                {
                    return NotFound($"IndustrySegments with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<IndustrySegmentsByIdResponse>(industrySegments, "IndustrySegments updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIndustrySegments(DeleteIndustrySegmentsReqest request)
        {
            try
            {
                var industrySegments = await _industrySegmentsService.DeleteAsync(request.Id);
                if (industrySegments == null)
                {
                    return NotFound($"IndustrySegments with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<object>(industrySegments, "IndustrySegments deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }
}
