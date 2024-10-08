using Maxus.Application.DTOs.UserFromRight;

namespace Maxus.Application.Interfaces
{
    public interface IUserFormRightService
    {
        Task<UserFormRightByIdResponse> CreateAsync(CreateUserFormRightRequest obj);

        Task<UserFormRightByIdResponse?> GetByIdAsync(GetUserFormRightRequest id);

        Task<bool> UpdateAsync(long id, UpdateUserFormRightRequest request);
    }
}
