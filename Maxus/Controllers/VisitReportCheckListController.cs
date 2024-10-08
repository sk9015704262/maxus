using AccountingAPI.Responses;
using Maxus.Application.DTOs.VisitReport;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class VisitReportCheckListController : BaseApiController
    {
        private readonly IVisitReportChekListService _visitReportService;

        public VisitReportCheckListController(IVisitReportChekListService visitReportService)
        {
            _visitReportService = visitReportService ?? throw new ArgumentNullException(nameof(visitReportService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisitReportCheckList(CreateVisitReportCheckListRequest createRequest)
        {
            try
            {
                var visit = await _visitReportService.CreateAsync(createRequest);
                if (visit == null)
                {
                    return BadRequest($"A visit report with the same name already exists.");
                }

                var response = new ApiResponse<VisitReportByIdResponse>(visit, "Visit report created successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetVisitReportCheckList(GetVisitReportRequestChekList getRequest)
        {
            try
            {
                var visit = await _visitReportService.GetByIdAsync(getRequest.Id);
                if (visit == null)
                {
                    return NotFound($"Visit with ID {getRequest.Id} not found.");
                }

                var response = new ApiResponse<VisitReportByIdResponse>(visit, "Visit fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVisitReportCheckList(UpdateVisitReportChekListRequest updateRequest)
        {
            try
            {
                var visit = await _visitReportService.UpdateAsync(updateRequest.Id, updateRequest);
                if (visit == null)
                {
                    return NotFound($"Visit with ID {updateRequest.Id} not found.");
                }

                var response = new ApiResponse<VisitReportByIdResponse>(visit, "Visit updated successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAllVisitReportCheckList(VisitReportChekListRequest listRequest)
        {
            try
            {
                var (paginationResponse, visits) = await _visitReportService.GetAllAsync(listRequest);
                var response = new ApiResponse<object>(new { visits, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "Visits fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVisitReportCheckList(DeleteVisitReportChekListRequest deleteRequest)
        {
            try
            {
                var visit = await _visitReportService.DeleteAsync(deleteRequest.Id);
                if (visit == null)
                {
                    return NotFound($"Visit with ID {deleteRequest.Id} not found.");
                }

                var response = new ApiResponse<object>(visit, "Visit deleted successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetvisitByCompanyCheckList(GetVisitByCompanyRequest Request)
        {
            try
            {
                var visit = await _visitReportService.GetVisitByCompanyIdAsync(Request , CurrentUserId);
                if (visit == null)
                {
                    return NotFound($"Visit with ID {Request.CompanyId} not found.");
                }

                var response = new ApiResponse<object>(visit, "Visit fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }
}
