using AccountingAPI.Application.Interfaces;
using Maxus.Application.DTOs.Auth;
using Maxus.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Maxus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthController(IAuthService authService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _authService = authService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("Login-admin")]
        public async Task<IActionResult> LoginAdmin(LoginDto LoginDto)
        {
            var user = await _authService.AdminLoginAsync(LoginDto);

            if (user != null)
            {
                var claims = new Claim[]
                  {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                   };
                var token = _jwtTokenGenerator.GenerateToken(claims);
                return Ok(new { User = user, token });
            }
            return Unauthorized(new { message = "Invalid username or password." });
        }


        [HttpPost("Login-user")]
        public async Task<IActionResult> LoginUser(LoginDto LoginDto)
        {
            var user = await _authService.UserLoginAsync(LoginDto);

            if (user != null)
            {
                var claims = new Claim[]
                  {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                   };
                var token = _jwtTokenGenerator.GenerateToken(claims);
                return Ok(new { User = user, token });
            }
            return Unauthorized(new { message = "Invalid username or password." });
        }
    }
}
