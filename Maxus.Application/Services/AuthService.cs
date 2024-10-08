using AccountingAPI.Application.Interfaces;
using AutoMapper;
using Maxus.Application.DTOs.Auth;
using Maxus.Application.DTOs.Branch;
using Maxus.Application.DTOs.Users;
using Maxus.Application.Interfaces;
using Maxus.Domain.Entities;
using Maxus.Domain.Interfaces;

namespace Maxus.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;

        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }



        public async Task<UserByIdResponse> AdminLoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _authRepository.AdminLoginAsync(loginDto.Email);
                if (user != null && _passwordHasher.VerifyPassword(user.Password, loginDto.Password))
                {
                    return _mapper.Map<UserByIdResponse>(user);
                }

                // Authentication failed
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Login User Error In Service.", ex);
            }
        }


        public async Task<UserByIdResponse> UserLoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _authRepository.UserLoginAsync(loginDto.Email);
                if (user != null && _passwordHasher.VerifyPassword(user.Password, loginDto.Password))
                {
                    return _mapper.Map<UserByIdResponse>(user);
                }

                // Authentication failed
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Login User Error In Service.", ex);
            }
        }

    }
}
