using AccountingAPI.Responses;
using Maxus.Application.DTOs.MOM;
using Maxus.Application.DTOs.TrainingReport;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class MOMReportController : ControllerBase
    {
        private readonly IMOMReportService _momReportService;

        public MOMReportController(IMOMReportService momReportService)
        {
            _momReportService = momReportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMOMReport(CreateMOMRequest request)
        {
            try
            {
                var momReport = await _momReportService.CreateAsync(request);
                if (momReport == 0)
                {
                    return BadRequest($"MOM Report is already created for the same site.");
                }

                if (momReport == 1)
                {
                    return Ok(new ApiResponse<object>(true, "MOM Report Updated successfully."));
                }

                return Ok(new ApiResponse<object>(true, "MOM Report created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }


        //new method create for get all mom report by company
        [HttpPost]
        public async Task<IActionResult> GetMOMReportByCompany(GetMOMReportByCompanyIdRequest request)
        {
            try
            {
                var momReport = await _momReportService.GetMOMReportByCompanyAsync(request);
                if (momReport == null)
                {
                    return NotFound($"MOM Report with Company ID {request.CompanyId} not found.");
                }

                return Ok(new ApiResponse<object>(new { momReport }, "MOM Report fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //new method create for get all mom report 
        [HttpPost]
        public async Task<IActionResult> GetAllMOMReport(GetMOMReportRequest request)
        {
            try
            {
                var (paginationResponse, MOMReports) = await _momReportService.GetAllAsync(request);
                if (MOMReports == null)
                {
                    return NotFound($"No MOM Report found.");
                }
                return Ok(new ApiResponse<object>(new { MOMReports, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "MOM Report fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //create a new method for get mom report

        [HttpPost]
        public async Task<IActionResult> GetMOMReport(GetMOMReportIdRequest request)
        {
            try
            {
                var momReport = await _momReportService.GetByIdAsync(request.Id);
                if (momReport == null)
                {
                    return NotFound($"MOM Report with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<GetMOMByIdResponse>(momReport, "MOM Report fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

      
    }
}
    