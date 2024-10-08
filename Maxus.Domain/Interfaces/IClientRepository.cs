using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using System.Runtime.InteropServices;

namespace Maxus.Domain.Interfaces
{
    public interface IClientRepository : IBaseRepository<tbl_ClientMaster>
    {
        Task<(FilterRecordsResponse, IEnumerable<tbl_ClientMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, long CompanyId, int? SearchColumn);

        

    }
}
