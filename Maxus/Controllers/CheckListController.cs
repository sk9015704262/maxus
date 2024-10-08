using AccountingAPI.Responses;
using Maxus.Application.DTOs.CustomerFeedbackOption;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class CheckListController : ControllerBase
    {
        private readonly ICustomerFeedbackOptionService _customerFeedbackOptionService;

        public CheckListController(ICustomerFeedbackOptionService customerFeedbackOptionService)
        {
            _customerFeedbackOptionService = customerFeedbackOptionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChekList(CreateCustomerFeedbackOptionRequest createRequest)
        {
            try
            {
                var feedbackOption = await _customerFeedbackOptionService.CreateAsync(createRequest);
                if (feedbackOption == null)
                {
                    return BadRequest($"A customer feedback option with the same name already exists.");
                }

                var response = new ApiResponse<CustomerFeedbackOptionByIdResponse>(feedbackOption, "Customer feedback created successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetChekList(GetCustomerFeedbackOptionRequest getRequest)
        {
            try
            {
                var feedbackOption = await _customerFeedbackOptionService.GetByIdAsync(getRequest.Id);
                if (feedbackOption == null)
                {
                    return NotFound($"Customer feedback option with ID {getRequest.Id} not found.");
                }

                var response = new ApiResponse<CustomerFeedbackOptionByIdResponse>(feedbackOption, "Customer feedback option fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAllChekList(CustomerFeedbackOptionListRequest request)
        {
            try
            {
                var (paginationResponse, feedbackOptions) = await _customerFeedbackOptionService.GetAllAsync(request);
                return Ok(new ApiResponse<object>(new { feedbackOptions, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "Customer feedback options fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChekList(UpdateCustomerFeedbackOptionRequest request)
        {
            try
            {
                var feedbackOption = await _customerFeedbackOptionService.UpdateAsync(request.Id, request);
                if (feedbackOption == null)
                {
                    return NotFound($"Customer feedback option with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<CustomerFeedbackOptionByIdResponse>(feedbackOption, "Customer feedback option updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }


        }
       
        [HttpPost]  
        public async Task<IActionResult> DeleteChekList(DeleteCustomerFeedbackOptionRequest request)
        {
            try
            {
                var feedbackOption = await _customerFeedbackOptionService.DeleteAsync(request.Id);
                if (feedbackOption == null)
                {
                    return NotFound($"Customer feedback option with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<object>(feedbackOption, "Customer feedback option deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }
}
