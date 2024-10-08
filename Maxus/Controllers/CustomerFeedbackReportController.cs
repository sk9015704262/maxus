using AccountingAPI.Responses;
using Maxus.Application.DTOs.CustomerFeedbackReport;
using Maxus.Application.DTOs.MOM;
using Maxus.Application.DTOs.VisitReport;
using Maxus.Application.Interfaces;
using Maxus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class CustomerFeedbackReportController : ControllerBase
    {
        private readonly ICustomerFeedbackReportService _customerFeedbackReportService;

        public CustomerFeedbackReportController(ICustomerFeedbackReportService customerFeedbackReportService)
        {
            _customerFeedbackReportService = customerFeedbackReportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerFeedbackReport(CreateCustomerFeedbackReportRequest request)
        {
            try
            {
                var customerFeedbackReport = await _customerFeedbackReportService.CreateCustomerFeedbackReport(request);
                if (customerFeedbackReport == 0)
                {
                    return BadRequest($"Customer Feedback Report is already created with the same name.");
                }

                if (customerFeedbackReport == 1)
                {
                    return Ok(new ApiResponse<object>(true, "Customer Feedback Report Updated successfully."));
                }
                return Ok(new ApiResponse<object>(true, "Customer Feedback Report created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

       //create mrthod for get all customer feedback report
       [HttpPost]
        public async Task<IActionResult> GetAllCustomerFeedbackReport(CustomerFeedbackReportListRequest request)
        {
            try
            {
                var (paginationResponse, customerFeedbackReports) = await _customerFeedbackReportService.GetAllAsync(request);
                var response = new ApiResponse<object>(new { customerFeedbackReports, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "Customer Feedback Reports fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //create method for get customer feedback report by id
        [HttpPost]
        public async Task<IActionResult> GetCustomerFeedbackReportById(GetCustomerFeedbackReportByIdRequest request)
        {
            try
            {
                var Report = await _customerFeedbackReportService.GetByIdAsync(request.Id);
                if (Report == null)
                {
                    return BadRequest("customer feedback report not found.");
                }

                var response = new ApiResponse<CustomerFeedbackReportByIdResponse>(Report, "customer feedback report fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

      
    }
}
    