using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;

namespace Maxus.Domain.Interfaces
{
    public interface ICustomerFeedBackRepository : IBaseRepository<tbl_CustomerFeedbackMaster>
    {
        Task<(FilterRecordsResponse, IEnumerable<tbl_CustomerFeedbackMaster>)> GetAllAsync(int pageNumber, int pageSize, int sortBy, string sortDir, string searchTerm,int companyId  , int? SearchColumn);
        Task<IEnumerable<tbl_CustomerFeedbackMaster>> GetCustomerFeedbackByCompanyAsync(int UserId , int CompanyId);
    }
}
