using Maxus.Application.DTOs.Auth;
using Maxus.Application.DTOs.Users;
using Maxus.Domain.Entities;

namespace AccountingAPI.Application.Interfaces
{
    public interface IAuthService
    { 
        Task<UserByIdResponse> AdminLoginAsync(LoginDto loginDto);

        Task<UserByIdResponse> UserLoginAsync(LoginDto loginDto);

    }
}
