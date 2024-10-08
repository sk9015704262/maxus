using AccountingAPI.Responses;
using Maxus.Application.DTOs.Topic;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService ?? throw new ArgumentNullException(nameof(topicService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic(CreateTopicRequest request)
        {
            try
            {
                var topic = await _topicService.CreateAsync(request);
                if (topic == null)
                {
                    return BadRequest($"Topic is already created with the same name");
                }

                return Ok(new ApiResponse<TopicByIdResponse>(topic, "Topic created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAllTopic(TopicListRequest request)
        {
            try
            {
                var (paginationResponse, topic) = await _topicService.GetAllAsync(request);
                return Ok(new ApiResponse<object>(new { topic, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "Topics fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> GetTopic(GetTopicReqest request)
        {
            try
            {
                var topic = await _topicService.GetByIdAsync(request.Id);
                if (topic == null)
                {
                    return NotFound($"Topic with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<TopicByIdResponse>(topic, "Topic fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTopic(UpdateTopicRequest request)
        {
            try
            {
                var topic = await _topicService.UpdateAsync(request.Id, request);
                if (topic == null)
                {
                    return NotFound($"Topic with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<TopicByIdResponse>(topic, "Topic updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTopic(DeleteTopicReqest request)
        {
            try
            {
                var topic = await _topicService.DeleteAsync(request.Id);
                if (topic == null)
                {
                    return NotFound($"Topic with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<object>(topic, "Topic deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }
}
