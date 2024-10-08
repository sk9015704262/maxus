using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<tbl_CompanyMaster>
    {
        Task<IEnumerable<tbl_UserCompany>> GetCompanyByUser(long UserId);
    }
}
