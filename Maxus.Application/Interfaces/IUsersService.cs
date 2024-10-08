using Maxus.Application.DTOs.Users;

namespace Maxus.Application.Interfaces
{
    public interface IUsersService : IBaseService<UsersListRequest, UsersListResponse, UserByIdResponse, CreateUserRequest, UpdateUserRequest>
    {
        Task<bool> RestPassword(ResetpasswordRequest request);
    }
}
