using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using System.ComponentModel.Design;

namespace Maxus.Domain.Interfaces
{
    public interface ISiteRepository : IBaseRepository<tbl_SiteMaster>
    {
        Task<IEnumerable<tbl_SiteMaster>> GetByCompanyAndClientIdAsync(int CompanyId, long ClientId);
        Task<IEnumerable<tbl_SiteMaster>> GetByUserIdAsync(int UserId, int Comapnyid);

        Task<IEnumerable<tbl_ClientRepresentativeDetails>> GetRepresentativeBySiteIdAsync(int SiteId);

        Task<IEnumerable<tbl_SiteMaster>> GetSiteByCompany(int CompanyId);
        Task<(FilterRecordsResponse, IEnumerable<tbl_SiteMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm, int? SearchColumn, long? CompanyId);
    }
}
