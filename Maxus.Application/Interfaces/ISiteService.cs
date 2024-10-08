using Maxus.Application.DTOs.Site;
using Maxus.Domain.DTOs;

namespace Maxus.Application.Interfaces
{
    public interface ISiteService : IBaseService<SiteListRequest, SiteListResponse, SiteByIdResponse, CreateSiteRequest, UpdateSiteRequest>
    {

        Task<IEnumerable<SiteListResponse>> GetByCompanyAndClientIdAsync(GetSiteByCompanyAndClientRequest obj);
        Task<IEnumerable<GetSiteByUserResponse>?> GetByUserIdAsync(GetSiteByUserRequest getSiteByUserRequest);

        Task<IEnumerable<ClientRepresentativeDetails>?> GetRepresentativeBySiteIdAsync(GetRepresentativeBySiteRequest request);

        Task<IEnumerable<SiteListResponse>?> GetSiteByCompany(GetSiteByCompanyRequest request);
    }
}
