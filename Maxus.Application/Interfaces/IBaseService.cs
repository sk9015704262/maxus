using Maxus.Domain.DTOs;

namespace Maxus.Application.Interfaces
{
    public interface IBaseService<TListRequest, TListResponse, TByIdResponse, TCreateDto, TUpdateRequest>
    {
        Task<(FilterRecordsResponse, IEnumerable<TListResponse>)> GetAllAsync(TListRequest obj);
        Task<TByIdResponse?> GetByIdAsync(int id);
        Task<TByIdResponse> CreateAsync(TCreateDto obj);
        Task<TByIdResponse> UpdateAsync(int id,  TUpdateRequest request);
        Task<bool> DeleteAsync(int id);

    }
}