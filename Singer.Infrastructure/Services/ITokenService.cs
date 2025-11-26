using System.Security.Claims;

namespace Singer.Infrastructure.Services;

public interface ITokenService
{
    string GenerateToken(IEnumerable<Claim> claims, DateTime expiresAtUtc);
}