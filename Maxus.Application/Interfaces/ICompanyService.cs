using Maxus.Application.DTOs.Company;

namespace Maxus.Application.Interfaces
{
    public interface ICompanyService : IBaseService<CompanyListRequest, CompanyListResponse, CompanyByIdResponse, CreateCompanyRequest, UpdateCompanyRequest>
    {
        Task<IEnumerable<CompanyListResponseByUser>> GetCompanyByUser(GetCompanyByUserRequest obj);
    }

}
