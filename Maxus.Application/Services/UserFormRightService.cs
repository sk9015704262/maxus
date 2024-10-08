using AutoMapper;
using Maxus.Application.DTOs.UserFromRight;
using Maxus.Application.Interfaces;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;

namespace Maxus.Application.Services
{
    public class UserFormRightService : IUserFormRightService
    {
        private readonly IUserFormRepository _userFormRepository;
        private readonly IMapper _mapper;

        public UserFormRightService(IUserFormRepository userFormRepository, IMapper mapper)
        {
            this._userFormRepository = userFormRepository;
            this._mapper = mapper;
        }

        public async Task<UserFormRightByIdResponse> CreateAsync(CreateUserFormRightRequest request)
        {
            try
            {
                var userFormRight = new tbl_UserFormRights
                {
                    UserId = request.UserId,
                    FormId = request.FormId,
                    CanView = request.CanView,
                    CanEdit = request.CanEdit,
                    CanAdd = request.CanAdd,
                    CanDelete = request.CanDelete,
                    CanExport = request.CanExport

                };
                var createdUserFormRight = await _userFormRepository.CreateAsync(userFormRight);
                return _mapper.Map<UserFormRightByIdResponse>(createdUserFormRight);
            }
            catch (Exception ex)
            {
                throw new Exception("Create User Form Right Error In Service.", ex);
            }
        }

        public async Task<UserFormRightByIdResponse?> GetByIdAsync(GetUserFormRightRequest request)
        {
            try
            {
                var userFormRight = await _userFormRepository.GetByIdAsync(request.userId, request.FormId);
                return _mapper.Map<UserFormRightByIdResponse>(userFormRight);
            }
            catch (Exception ex)
            {
                throw new Exception("Get User Form Right By Id Error In Service.", ex);
            }
        }

        public async Task<bool> UpdateAsync(long id, UpdateUserFormRightRequest request)
        {
            try
            {
                var userFormRight = new tbl_UserFormRights
                {
                    UserId = request.UserId,
                    FormId = request.FormId,
                    CanView = request.CanView,
                    CanEdit = request.CanEdit,
                    CanAdd = request.CanAdd,
                    CanDelete = request.CanDelete,
                    CanExport = request.CanExport
                };

                var success = await _userFormRepository.UpdateAsync(userFormRight);
                if (success)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Update User Form Right Error In Service.", ex);
            }
        }
    }
}
