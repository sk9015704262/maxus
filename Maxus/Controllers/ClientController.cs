using AccountingAPI.Responses;
using Maxus.Application.DTOs.Client;
using Maxus.Application.DTOs.Company;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService ;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientRequest request)
        {
            try
            {
                var client = await _clientService.CreateAsync(request);
                if (client == null)
                {
                    return BadRequest($"Client is already created with the same name");
                }

                return Ok(new ApiResponse<ClientByIdResponse>(client, "Client created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAllClients(ClientListRequest request)
        {
            try
            {
                var (paginationResponse, clients) = await _clientService.GetAllAsync(request);
                return Ok(new ApiResponse<object>(new { Clients = clients, TotalRecords = paginationResponse.TotalRecords, FilteredRecords = paginationResponse.FilteredRecords }, "Clients fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> GetClient(GetCompanyRequest request)
        {
            try
            {
                var client = await _clientService.GetByIdAsync(request.Id);
                if (client == null)
                {
                    return NotFound($"Client with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<ClientByIdResponse>(client, "Client fetched successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClient(UpdateClientRequest request)
        {
            try
            {
                var client = await _clientService.UpdateAsync(request.Id, request);
                if (client == null)
                {
                    return NotFound($"Client with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<ClientByIdResponse>(client, "Client updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClient(DeleteClientReqest request)
        {
            try
            {
                var client = await _clientService.DeleteAsync(request.Id);
                if (client == null)
                {
                    return NotFound($"Client with ID {request.Id} not found.");
                }

                return Ok(new ApiResponse<object>(client, "Client deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<object>(null, ex.Message));
            }
        }


        //get client by company id 
        
    }
}
