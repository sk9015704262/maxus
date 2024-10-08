using Maxus.Application.DTOs.AttachmentLimits;
using Maxus.Domain.DTOs;

namespace Maxus.Application.Interfaces
{
    public interface IAttachmentLimitsService
    {
        Task<AttachmentLimitByIdResponse> CreateAsync(CreateAttachmentLimitRequest obj);

        Task<AttachmentLimitByIdResponse?> GetByIdAsync(GetAttachmentBySiteRequest request);

        Task<AttachmentLimitByIdResponse?> GetById(int id);

        Task<bool> DeleteAsync(int id);

        Task<(FilterRecordsResponse, IEnumerable<AttachmentLimitListResponse>)> GetAllAsync(AttachmentLimitListRequest obj);

        Task<AttachmentLimitByIdResponse> UpdateAsync(int id, UpdateAttachmentLimitRequest request);
    }
}
