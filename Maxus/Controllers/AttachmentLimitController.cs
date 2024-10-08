using AccountingAPI.Responses;
using Maxus.Application.DTOs.AttachmentLimits;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AttachmentLimitController : ControllerBase
    {
        private readonly IAttachmentLimitsService _attachmentLimitsService;

        public AttachmentLimitController(IAttachmentLimitsService attachmentLimitsService)
        {
            _attachmentLimitsService = attachmentLimitsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttachmentLimit(CreateAttachmentLimitRequest createRequest)
        {
            try
            {
                var attachmentLimit = await _attachmentLimitsService.CreateAsync(createRequest);
                if (attachmentLimit == null)
                {
                    return BadRequest($"Attachment limit is already created with the same name.");
                }

                return Ok(new ApiResponse<AttachmentLimitByIdResponse>(attachmentLimit, "Attachment limit created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAttachmentBySite(GetAttachmentBySiteRequest getRequest)
        {
            try
            {
                var attachmentLimit = await _attachmentLimitsService.GetByIdAsync(getRequest);
                if (attachmentLimit == null)
                {
                    return BadRequest($"Attachment limit By Site Id Not Found.");
                }

                return Ok(new ApiResponse<AttachmentLimitByIdResponse>(attachmentLimit, "Attachment limit fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //create get all attachment limit
        [HttpPost]
        public async Task<IActionResult> GetAllAttachmentLimit(AttachmentLimitListRequest request)
        {
            try
            {
                var (paginationResponse, attachmentLimits) = await _attachmentLimitsService.GetAllAsync(request);
                var response = new ApiResponse<object>(new { attachmentLimits, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "Attachment Limits fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //add new method for update attachment limit
        [HttpPost]
        public async Task<IActionResult> UpdateAttachmentLimit(UpdateAttachmentLimitRequest updateRequest)
        {
            try
            {
                var attachmentLimit = await _attachmentLimitsService.UpdateAsync(updateRequest.Id , updateRequest);
                if (attachmentLimit == null)
                {
                    return BadRequest($"Attachment limit is already created with the same name.");
                }

                return Ok(new ApiResponse<AttachmentLimitByIdResponse>(attachmentLimit, "Attachment limit updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //create new method for get by id    attachment limit
        [HttpPost]
        public async Task<IActionResult> GetByIdAttachmentLimit(GetAttachmentByIdRequest getRequest)
        {
            try
            {
                var attachmentLimit = await _attachmentLimitsService.GetById(getRequest.Id);
                if (attachmentLimit == null)
                {
                    return BadRequest($"Attachment limit By Id Not Found.");
                }

                return Ok(new ApiResponse<AttachmentLimitByIdResponse>(attachmentLimit, "Attachment limit fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //create new method delete attachment limit
        [HttpPost]
        public async Task<IActionResult> DeleteAttachmentLimit(DeleteAttachmentRequest deleteRequest)
        {
            try
            {
                var attachmentLimit = await _attachmentLimitsService.DeleteAsync(deleteRequest.Id);
                if (attachmentLimit == null)
                {
                    return BadRequest($"Attachment limit is already created with the same name.");
                }

                return Ok(new ApiResponse<object>(attachmentLimit, "Attachment limit deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

    }
}
