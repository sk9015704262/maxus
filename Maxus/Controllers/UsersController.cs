using AccountingAPI.Responses;
using Maxus.Application.DTOs.Branch;
using Maxus.Application.DTOs.Users;
using Maxus.Application.Interfaces;
using Maxus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {

            try
            {
                var user = await _usersService.CreateAsync(request);
                if (user == null)
                {
                    return BadRequest("User is already created with the same name");
                }

                return Ok(new ApiResponse<UserByIdResponse>(user, "User created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }


        //create new method for get all user
        [HttpPost]
        public async Task<IActionResult> GetAllUser(UsersListRequest request)
        {
            try
            {
                var (paginationResponse, users) = await _usersService.GetAllAsync(request);
                return Ok(new ApiResponse<object>(new { users, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "Users fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
        //create new method for update user

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            try
            {
                var user = await _usersService.UpdateAsync(request.Id , request);
                if (user == null)
                {
                    return BadRequest("User is already created with the same name");
                }

                return Ok(new ApiResponse<UserByIdResponse>(user, "User updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        //create new method for delete user
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _usersService.DeleteAsync(id);
                if (result)
                {
                    return Ok(new ApiResponse<object>(null, "User deleted successfully."));
                }
                return BadRequest("Error deleting user.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetUser(GetUserReqest request)
        {
            try
            {
                var user = await _usersService.GetByIdAsync(request.Id);
                if (user == null)
                {
                    return NotFound($"User with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<UserByIdResponse>(user, "User fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetpasswordRequest request)
        {
            try
            {
                var user = await _usersService.RestPassword(request);
                if (user == null)
                {
                    return NotFound($"User with ID {request.UserId} not found.");
                }

                return Ok(new ApiResponse<object>(user, "User Password Reset successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }


    }
}
