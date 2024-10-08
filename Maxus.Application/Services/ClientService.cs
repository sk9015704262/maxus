using AutoMapper;
using Maxus.Application.DTOs.Client;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ClientService(IClientRepository clientRepository, IMapper mapper , IHttpContextAccessor httpContextAccessor)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<ClientByIdResponse> CreateAsync(CreateClientRequest request)
        {
            try
            {
                var client = new tbl_ClientMaster
                {
                    CompanyId = request.CompanyId,
                    Code = request.Code,
                    Name = request.Name,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserId()
                };

                var createdClient = await _clientRepository.CreateAsync(client);
                return _mapper.Map<ClientByIdResponse>(createdClient);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while creating client.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await _clientRepository.DeleteAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting client.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<ClientListResponse>)> GetAllAsync(ClientListRequest request)
        {
            try
            {
                var (paginationResponse, clients) = await _clientRepository.GetAllAsync(request.PageNumber, request.PageSize, request.SortBy, request.SortDir, request.SearchTerm , request.CompanyId , request.SearchColumn);
                var clientListResponse = _mapper.Map<IEnumerable<ClientListResponse>>(clients);
                return (paginationResponse, clientListResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving all clients.", ex);
            }
        }

        public async Task<ClientByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                return _mapper.Map<ClientByIdResponse>(client);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while retrieving client by ID.", ex);
            }
        }

        public async Task<ClientByIdResponse> UpdateAsync(int id, UpdateClientRequest request)
        {
            try
            {
                var client = await _clientRepository.GetByIdAsync(id);
                if (client != null)
                {
                    client.CompanyId = request.CompanyId;
                    client.UpdatedAt = DateTime.Now;
                    client.UpdatedBy = CurrentUserId();
                    client.Name = request.Name;
                    client.Code = request.Code;
                    var success = await _clientRepository.UpdateAsync(client);
                    if (success)
                    {
                        return _mapper.Map<ClientByIdResponse>(client);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating client.", ex);
            }
        }

        
    }
}
