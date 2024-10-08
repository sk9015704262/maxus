using System.Security.Claims;

namespace Maxus.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Claim[] claims);
        string GenerateRefreshToken();
    }
}
