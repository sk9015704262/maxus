using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface IAttachmentLimitRepository
    {
        Task<tbl_AttachmentLimits> CreateAsync(tbl_AttachmentLimits obj);

        Task<tbl_AttachmentLimits?> GetByIdAsync(int id , int SiteId);

        Task<tbl_AttachmentLimits?> GetById(int id);

        Task<bool> DeleteAsync(int id);

        Task<(FilterRecordsResponse, IEnumerable<tbl_AttachmentLimits>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm , int? SearchColumn);

        Task<bool> UpdateAsync(tbl_AttachmentLimits obj);
    }
}
