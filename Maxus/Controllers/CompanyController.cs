using AccountingAPI.Responses;
using Maxus.Application.DTOs.Company;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        [HttpPost]
        public async Task<IActionResult> GetAllCompanies(CompanyListRequest request)
        {
            try
            {
                var (paginationResponse, companies) = await _companyService.GetAllAsync(request);
                var response = new ApiResponse<object>(new { companies, totalRecords = paginationResponse.TotalRecords, filteredRecords = paginationResponse.FilteredRecords }, "Companies fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCompany(GetCompanyRequest request)
        {
            try
            {
                var company = await _companyService.GetByIdAsync(request.Id);
                if (company == null)
                {
                    return NotFound($"Company with ID {request.Id} not found.");
                }

                var response = new ApiResponse<CompanyByIdResponse>(company, "Company fetched successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyRequest request)
        {
            try
            {

                var company = await _companyService.CreateAsync(request);
                if (company == null)
                {   
                    return BadRequest($"Company is already created with the same name.");
                }

                var response = new ApiResponse<CompanyByIdResponse>(company, "Company created successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCompany(UpdateCompanyRequest request)
        {
            try
            {
                var company = await _companyService.UpdateAsync(request.Id, request);
                if (company == null)
                {
                    return NotFound($"Company with ID {request.Id} not found.");
                }

                var response = new ApiResponse<CompanyByIdResponse>(company, "Company updated successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCompany(DeleteCompanyRequest request)
        {
            try
            {
                var company = await _companyService.DeleteAsync(request.Id);
                if (company == null)
                {
                    return NotFound($"Company with ID {request.Id} not found.");
                }

                var response = new ApiResponse<object>(company, "Company deleted successfully.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCompanyByUser(GetCompanyByUserRequest request)
        {
            try
            {
                var company = await _companyService.GetCompanyByUser(request);
                var response = new ApiResponse<object>(new { company }, "Companies fetched successfully for the user.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

    }
}
