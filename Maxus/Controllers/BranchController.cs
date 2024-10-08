using AccountingAPI.Responses;
using Maxus.Application.DTOs.Branch;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService ?? throw new ArgumentNullException(nameof(branchService));
        }

        [HttpPost]
        public async Task<IActionResult> GetAllBranch(BranchListRequest request)
        {
            try
            {
                var (paginationResponse, branches) = await _branchService.GetAllAsync(request);
                var response = new ApiResponse<object>(new
                {
                    branches,
                    totalRecords = paginationResponse.TotalRecords,
                    filteredRecords = paginationResponse.FilteredRecords
                }, "Branches fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(CreateBranchRequest request)
        {
            try
            {
                var branch = await _branchService.CreateAsync(request);
                if (branch == null)
                {
                    return BadRequest($"Branch is already created with the same name.");
                }

                return Ok(new ApiResponse<BranchByIdResponse>(branch, "Branch created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBranch(UpdateBranchRequest request)
        {
            try
            {
                var branch = await _branchService.UpdateAsync(request.Id, request);
                if (branch == null)
                {
                    return NotFound($"Branch with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<BranchByIdResponse>(branch, "Branch updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetBranch(GetBranchRequest request)
        {
            try
            {
                var branch = await _branchService.GetByIdAsync(request.Id);
                if (branch == null)
                {
                    return NotFound($"Branch with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<BranchByIdResponse>(branch, "Branch fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBranch(DeleteBranchRequest request)
        {
            try
            {
                var branch = await _branchService.DeleteAsync(request.Id);
                if (branch == null)
                {
                    return NotFound($"Branch with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<object>(branch, "Branch deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }
    }
}
