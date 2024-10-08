using AccountingAPI.Responses;
using Maxus.Application.DTOs.UserRights;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class UserRightsController : ControllerBase
    {
        private readonly IUserRightsService _userRightsService;

        public UserRightsController(IUserRightsService userRightsService)
        {
            this._userRightsService = userRightsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserRight(CreateUserRightRequest request)
        {
            try
            {
                var userRight = await _userRightsService.CreateAsync(request);
                if (userRight == null)
                {
                    return BadRequest($"User right is already created with the same name.");
                }

                return Ok(new ApiResponse<UserRightsByIdResponse>(userRight, "User right created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetUserRight(GetUserRightsReqest request)
        {
            try
            {
                var userRight = await _userRightsService.GetByIdAsync(request.Id);
                if (userRight == null)
                {
                    return NotFound($"User right with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<UserRightsByIdResponse>(userRight, "User right fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRight(UpdateUserRightRequest request)
        {
            try
            {
                var userRight = await _userRightsService.UpdateAsync(request.Id, request);
                if (userRight == null)
                {
                    return BadRequest($"User right is already created with the same name");
                }

                return Ok(new ApiResponse<bool>(userRight, "User right updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //create new method for get all user right
        [HttpPost]
        public async Task<IActionResult> GetAllUserRight(GetUserRightListRequest request)
        {
            try
            {
                var (paginationResponse, userRight ) = await _userRightsService.GetAllAsync(request);


                return Ok(new ApiResponse<object>(new { userRight, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "User Right fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //add new method for delete user right
        [HttpPost]
        public async Task<IActionResult> DeleteUserRight(DeleteUserRightRequest request)
        {
            try
            {
                var userRight = await _userRightsService.DeleteAsync(request.Id);
                if (userRight == null)
                {
                    return BadRequest($"User right is already created with the same name.");
                }

                return Ok(new ApiResponse<bool>(userRight, "User right deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }
}
