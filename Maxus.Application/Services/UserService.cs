using AutoMapper;
using Maxus.Application.DTOs.Branch;
using Maxus.Application.DTOs.TrainingReport;
using Maxus.Application.DTOs.Users;
using Maxus.Application.Interfaces;
using Maxus.Domain.DTOs;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Maxus.Application.Services
{
    public class UserService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _hashPassword;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher hashPassword, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _hashPassword = hashPassword;
            _httpContextAccessor = httpContextAccessor;
        }

        protected int CurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true ? Convert.ToInt32(user.Identity.Name) : 0;
        }

        public async Task<UserByIdResponse> CreateAsync(CreateUserRequest obj)
        {
            try
            {
                obj.Password = _hashPassword.HashPassword(obj.Password);

                var user = new tbl_Users
                {
                    Name = obj.Name,
                    PhoneNo = obj.PhoneNo,
                    Email = obj.Email,
                    Designation = obj.Designation,
                    Username = obj.Username,
                    Password = obj.Password,
                    CreatedAt = DateTime.Now,
                    CreatedBy = CurrentUserId(),
                    CompanyId = obj.CompanyId,
                    RoleId = obj.RoleId

                };

                var createdUser = await _userRepository.CreateAsync(user);
                return _mapper.Map<UserByIdResponse>(createdUser);
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
                return await _userRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Delete user Error In Service.", ex);
            }
        }

        public async Task<(FilterRecordsResponse, IEnumerable<UsersListResponse>)> GetAllAsync(UsersListRequest request)
        {
            try
            {
                var (paginationResponse, branches) = await _userRepository.GetAllAsync(request.PageNumber, request.PageSize, request.SortBy, request.SortDir, request.SearchTerm , request.SearchColumn) ;
                return (paginationResponse, _mapper.Map<IEnumerable<UsersListResponse>>(branches));
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all user in service.", ex);
            }
        }

        public async Task<UserByIdResponse?> GetByIdAsync(int id)
        {
            try
            {
                var branch = await _userRepository.GetByIdAsync(id);
                return _mapper.Map<UserByIdResponse>(branch);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting User by id in service.", ex);
            }
        }

        public async Task<UserByIdResponse> UpdateAsync(int id, UpdateUserRequest request)
        {
            try
            {
               

                var TraningReport = new tbl_Users
                {
                    Id = id,
                    Name = request.Name,
                    PhoneNo = request.PhoneNo,
                    Email = request.Email,
                    Designation = request.Designation,
                    Username = request.Username,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = CurrentUserId(),
                    CompanyId = request.CompanyId,
                    RoleId = request.RoleId
                };
              

                var success = await _userRepository.UpdateAsync(TraningReport);
                if (success)
                {
                    return _mapper.Map<UserByIdResponse>(TraningReport);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Update User Error In Service.", ex);
            }
        }

        public async Task<bool> RestPassword(ResetpasswordRequest request)
        {
             var Password = _hashPassword.HashPassword(request.password);
            try
            {
                return await _userRepository.ResetPassword(request.UserId , Password);
            }
            catch (Exception ex)
            {
                throw new Exception("Reset password user Error In Service.", ex);
            }
        }
    }
}
