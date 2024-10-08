using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<tbl_Users>
    {
        Task<bool> ResetPassword(long UserId , string Password);
    }
}
