using Maxus.Application.DTOs.UserRights;
using Maxus.Domain.DTOs;

namespace Maxus.Application.Interfaces
{
    public interface IUserRightsService
    {
        Task<UserRightsByIdResponse> CreateAsync(CreateUserRightRequest obj);

        Task<UserRightsByIdResponse?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(long id, UpdateUserRightRequest request);

        Task<(FilterRecordsResponse, IEnumerable<UserRightListResponse>)> GetAllAsync(GetUserRightListRequest obj);

        Task<bool> DeleteAsync(int id);
    }
}
