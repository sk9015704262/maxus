using AccountingAPI.Responses;
using Maxus.Application.DTOs.dashboard;
using Maxus.Application.DTOs.MOM;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AdminDashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;

        public AdminDashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAdminDashBoardData(GetReportCountRequest request)
        {
            try
            {
                var Count = await _dashBoardService.Getcount(request.CompanyId);
                if (Count == null)
                {
                    return BadRequest($"error");
                }

                return Ok(new ApiResponse<object>(Count, "Count fached successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }
}