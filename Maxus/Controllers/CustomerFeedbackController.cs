using AccountingAPI.Responses;
using Maxus.Application.DTOs.CustomerFeedback;
using Maxus.Application.DTOs.CustomerFeedbackOption;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class CustomerFeedbackController : BaseApiController
    {
        private readonly ICustomerFeedbackService _customerFeedbackService;

        public CustomerFeedbackController(ICustomerFeedbackService customerFeedbackService)
        {
            _customerFeedbackService = customerFeedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerFeedback(CreateCustomerFeedbackRequest createRequest)
        {
            try
            {
                var feedback = await _customerFeedbackService.CreateAsync(createRequest);
                if (feedback == null)
                {
                    return BadRequest($"A customer feedback with the same name already exists.");
                }

                var response = new ApiResponse<CustomerFeedbackByIdResponse>(feedback, "Customer feedback created successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerFeedback(GetCustomerFeedbackRequest getRequest)
        {
            try
            {
                var feedback = await _customerFeedbackService.GetByIdAsync(getRequest.Id);
                if (feedback == null)
                {
                    return NotFound($"Customer feedback with ID {getRequest.Id} not found.");
                }

                var response = new ApiResponse<CustomerFeedbackByIdResponse>(feedback, "Customer feedback fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAllCustomerfeedback(CustomerFeedbackListRequest listRequest)
        {
            try
            {
                var (paginationResponse, feedbacks) = await _customerFeedbackService.GetAllAsync(listRequest);
                var response = new ApiResponse<object>(new { feedbacks, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "Customer feedbacks fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCustomerFeedback(UpdateCustomerFeedbackRequest updateRequest)
        {
            try
            {
                var feedback = await _customerFeedbackService.UpdateAsync(updateRequest.Id, updateRequest);
                if (feedback == null)
                {
                    return NotFound($"Customer feedback with ID {updateRequest.Id} not found.");
                }

                var response = new ApiResponse<CustomerFeedbackByIdResponse>(feedback, "Customer feedback updated successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomerFeedback(DeleteCustomerFeedbackRequest deleteRequest)
        {
            try
            {
                var feedback = await _customerFeedbackService.DeleteAsync(deleteRequest.Id);
                if (feedback == null)
                {
                    return NotFound($"Customer feedback with ID {deleteRequest.Id} not found.");
                }

                var response = new ApiResponse<object>(feedback, "Customer feedback deleted successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerFeedbackCheckList(GetCustomerFeedbackByCompanyIdRequest Request)
        {
            try
            {
                var feedback = await _customerFeedbackService.GetCustomerFeedbackByCompanyIdAsync(Request , CurrentUserId);
                if (feedback == null)
                {
                    return NotFound($"Customer feedback with ID {Request.CompanyId} not found.");
                }

                var response = new ApiResponse<object>(feedback, "Customer feedback fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }
}
