using AccountingAPI.Responses;
using Maxus.Application.DTOs.Topic;
using Maxus.Application.DTOs.TrainingReport;
using Maxus.Application.Interfaces;
using Maxus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class TrainingReportController : ControllerBase
    {
        private readonly ITrainingReportService _trainingReportService;

        public TrainingReportController(ITrainingReportService trainingReportService)
        {
            _trainingReportService = trainingReportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrainingReport(CreateTrainingReportRequest request)
        {
            try
            {
                var trainingReport = await _trainingReportService.CreateAsync(request);
                if (trainingReport == 0)
                {
                    return BadRequest($"Training report is already created with the same name");
                }

                if (trainingReport == 1)
                {
                    return Ok(new ApiResponse<object>(true, "Training report Updated successfully."));
                }

                return Ok(new ApiResponse<object>(true, "Training report created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

      

        [HttpPost]
        public async Task<IActionResult> GetAllTrainingReports(TrainingReportListRequest request)
        {
            try
            {
                var (paginationResponse, topic) = await _trainingReportService.GetAllAsync(request);
                return Ok(new ApiResponse<object>(new { topic, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "Training Reports fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> GetTrainingReportByCompanyId(GetTraningReportByCompanyIdRequest request)
        //{
        //    try
        //    {
        //        var trainingReport = await _trainingReportService.GetTraningByCompanyIdAsync(request);
        //        if (trainingReport == null)
        //        {
        //            return BadRequest($"Training report with ID {request.CompanyId} not found.");
        //        }

        //        return Ok(new ApiResponse<object>(trainingReport, "Training report fetched successfully."));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ApiResponse<object>(null, ex.Message));
        //    }
        //}

        //get training report by id post method use

        [HttpPost]
        public async Task<IActionResult> GetTrainingReportById(GetTrainingReportByIdRequest request)
        {
            try
            {
                var trainingReport = await _trainingReportService.GetByIdAsync(request.Id);
                if (trainingReport == null)
                {
                    return BadRequest($"Training report with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<object>(trainingReport, "Training report fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }
}
