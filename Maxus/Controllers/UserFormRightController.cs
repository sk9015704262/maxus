using AccountingAPI.Responses;
using Maxus.Application.DTOs.UserFromRight;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class UserFormRightController : ControllerBase
    {
        private readonly IUserFormRightService _userFormRightService;

        public UserFormRightController(IUserFormRightService userFormRightService)
        {
            this._userFormRightService = userFormRightService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserFormRight(CreateUserFormRightRequest request)
        {
            try
            {
                var userFormRight = await _userFormRightService.CreateAsync(request);
                if (userFormRight == null)
                {
                    return BadRequest($"User form right is already created with the same name.");
                }

                return Ok(new ApiResponse<UserFormRightByIdResponse>(userFormRight, "User form right created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetUserFormRight(GetUserFormRightRequest request)
        {
            try
            {
                var userFormRight = await _userFormRightService.GetByIdAsync(request);
                if (userFormRight == null)
                {
                    return NotFound($"User form right with ID {request.userId} not found.");
                }

                return Ok(new ApiResponse<UserFormRightByIdResponse>(userFormRight, "User form right fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateUserFormRight(UpdateUserFormRightRequest request)
        {
            try
            {
                var userFormRight = await _userFormRightService.UpdateAsync(request.UserId, request);
                if (userFormRight == null)
                {
                    return BadRequest($"User form right is already created with the same name");
                }

                return Ok(new ApiResponse<bool>(userFormRight, "User form right updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }
}
