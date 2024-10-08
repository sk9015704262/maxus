using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface IVisitReportChekListRepository : IBaseRepository<tbl_VisitReportChekListMaster>
    {
        Task<IEnumerable<tbl_VisitReportChekListMaster?>> GetVisitByComanyIdAsync(int userId , int companyId);
    }
}
