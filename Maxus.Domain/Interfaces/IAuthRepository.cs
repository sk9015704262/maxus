using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<tbl_Users> AdminLoginAsync(string Email);

        Task<tbl_Users> UserLoginAsync(string Email);


    }
}
