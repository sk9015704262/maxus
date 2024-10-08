using AutoMapper;
using Maxus.Application.DTOs.Topic;
using Maxus.Application.DTOs.UserRights;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class UserRightService : IUserRightsService
    {
        private readonly IUserRightRepository _userRightRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRightService(IUserRightRepository userRightRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this._userRightRepository = userRightRepository;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }
        public async Task<UserRightsByIdResponse> CreateAsync(CreateUserRightRequest request)
        {
            try
            {
                var userRight = new tbl_UserRights
                {
                    UserId = request.UserId,
                    RightTypeId = request.RightTypeId,
                    Details = request.Details,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserId()
                };

                var createdUserRight = await _userRightRepository.CreateAsync(userRight);
                return _mapper.Map<UserRightsByIdResponse>(createdUserRight);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating user right.", ex);
            }
        }

        public async Task<UserRightsByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var userRight = await _userRightRepository.GetByIdAsync(id);
                var finalObj = _mapper.Map<UserRightsByIdResponse>(userRight);
                //finalObj.Details = userRight.Details;
                return _mapper.Map<UserRightsByIdResponse>(userRight);
            }
            catch (Exception ex)
            {
                throw new Exception("GetById user right error in service.", ex);
            }
        }

        public async Task<bool> UpdateAsync(long id, UpdateUserRightRequest request)
        {
            try
            {
                var userRight = new tbl_UserRights
                {
                    UserId = request.Id,
                    RightTypeId = request.RightTypeId,
                    Details = request.Details,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = CurrentUserId()
                };


                var success = await _userRightRepository.UpdateAsync(userRight);
                if (success)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Update user right error in service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<UserRightListResponse>)> GetAllAsync(GetUserRightListRequest obj)
        {
            try
            {
                var (paginationResponse, UserRight) = await _userRightRepository.GetAllAsync(obj.PageNumber, obj.PageSize, obj.SortBy, obj.SortDir, obj.SearchTerm , obj.SearchColumn) ;
                return (paginationResponse, _mapper.Map<IEnumerable<UserRightListResponse>>(UserRight));
            }
            catch (Exception ex)
            {
                throw new Exception("GetAll UserRight Error In Service.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _userRightRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete user right error in service.", ex);
            }
        }
    }
}
