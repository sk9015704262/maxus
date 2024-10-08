using Maxus.Application.DTOs.Client;

namespace Maxus.Application.Interfaces
{
    public interface IClientService : IBaseService<ClientListRequest, ClientListResponse, ClientByIdResponse, CreateClientRequest, UpdateClientRequest>
    {     
    }
}
