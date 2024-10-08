using AccountingAPI.Responses;
using Maxus.Application.DTOs.MOM;
using Maxus.Application.DTOs.VisitReport;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class VisitReportController : ControllerBase
    {
        private readonly IVisitReportService _visitReportService;

        public VisitReportController(IVisitReportService visitReportService)
        {
            _visitReportService = visitReportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisitReport(CreateVisitReportRequest request)
        {
            try
            {
                var visit = await _visitReportService.CreateAsync(request);
                if (visit == 0)
                {
                    return BadRequest($"A visit report with the same name already exists.");
                }

                if (visit == 1)
                {
                    return Ok(new ApiResponse<object>(true, "Visit report Updated successfully."));
                }

                var response = new ApiResponse<object>(true, "Visit report created successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //create method get visit report all 
        [HttpPost]
        public async Task<IActionResult> GetAllVisitReport(VisitReportListRequest request)
        {
            try
            {
                var (paginationResponse, visitReports) = await _visitReportService.GetAllAsync(request);
                var response = new ApiResponse<object>(new { visitReports, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "Visit Reports fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //create method get visit report by id
        [HttpPost]
        public async Task<IActionResult> GetVisitReportById(GetVisitReportByIdRequest request)
        {
            try
            {
                var visitReport = await _visitReportService.GetByIdAsync(request.Id);
                if (visitReport == null)
                {
                    return BadRequest("Visit report not found.");
                }

                var response = new ApiResponse<VisitReportByidResponse>(visitReport, "Visit report fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

       
    }
}
