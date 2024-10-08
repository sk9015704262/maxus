using AutoMapper;
using Maxus.Application.DTOs.Branch;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public BranchService(IMapper mapper, IBranchRepository branchRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<BranchByIdResponse> CreateAsync(CreateBranchRequest request)
        {
            try
            {
                var branch = new tbl_BranchMaster
                {
                    CompanyId = request.CompanyId,
                    Code = request.Code,
                    Name = request.Name,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserId()
                };

                var createdBranch = await _branchRepository.CreateAsync(branch);
                return _mapper.Map<BranchByIdResponse>(createdBranch);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await _branchRepository.DeleteAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting branch in service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<BranchListResponse>)> GetAllAsync(BranchListRequest request)
        {
            try
            {
                var (paginationResponse, branches) = await _branchRepository.GetAllAsync(request.PageNumber, request.PageSize, request.SortBy, request.SortDir, request.SearchTerm, request.CompanyId , request.SearchColumn);
                return (paginationResponse, _mapper.Map<IEnumerable<BranchListResponse>>(branches));
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all branches in service.", ex);
            }
        }

        public async Task<BranchByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var branch = await _branchRepository.GetByIdAsync(id);
                return _mapper.Map<BranchByIdResponse>(branch);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting branch by id in service.", ex);
            }
        }

        public async Task<BranchByIdResponse> UpdateAsync(int id, UpdateBranchRequest request)
        {
            try
            {
                var branch = await _branchRepository.GetByIdAsync(id);
                if (branch != null)
                {
                    branch.CompanyId = request.CompanyId;
                    branch.UpdatedAt = DateTime.Now;
                    branch.UpdatedBy = CurrentUserId();
                    branch.Name = request.Name;
                    branch.Code = request.Code;
                    var success = await _branchRepository.UpdateAsync(branch);
                    if (success)
                    {
                        return _mapper.Map<BranchByIdResponse>(branch);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating branch in service.", ex);
            }
        }
    }
}
