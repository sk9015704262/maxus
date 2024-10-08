using Maxus.Application.DTOs.Branch;

namespace Maxus.Application.Interfaces
{
    public interface IBranchService : IBaseService<BranchListRequest, BranchListResponse, BranchByIdResponse, CreateBranchRequest, UpdateBranchRequest>
    {

    }
}
