using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Singer.DomainModel.RequestDTO;
using Singer.DomainModel.ResponseDTO;
using Singer.Infrastructure.Services;
using Singer.Persistance.Data.Entities;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Singer.BusinessLogic.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task Register(RegisterRequestDTO register)
    {
        User? existing = await _userManager.FindByNameAsync(register.UserName);
        if (existing is not null)
        {
            throw new ArgumentException("User already exists");
        }

        var user = new User
        {
            UserName = register.UserName,
            Email = register.Email,
            EmailConfirmed = true
        };

        IdentityResult result = await _userManager.CreateAsync(user, register.Password);

        if (!result.Succeeded)
        {
            throw new ArgumentException("Unsuccessful Registration");
        }

        //await _userManager.AddToRoleAsync(user, "User");
    }

    [HttpPost]
    public async Task<AuthenticationResponseDTO> Login(LoginRequestDTO login)
    {
        User? user = await _userManager.FindByNameAsync(login.UserName);
        if (user is null)
        {
            throw new ArgumentException("Invalid username or password.");
        }

        SignInResult signInResult =
            await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: false);

        if (!signInResult.Succeeded)
        {
            throw new ArgumentException("Invalid username or password.");
        }

        IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
        IList<string> roles = await _userManager.GetRolesAsync(user);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
        };

        claims.AddRange(userClaims);

        foreach (string role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        DateTime expiresAtUtc = DateTime.UtcNow.AddMinutes(60);
        string token = _tokenService.GenerateToken(claims, expiresAtUtc);

        AuthenticationResponseDTO response = new AuthenticationResponseDTO
        {
            AccessToken = token,
            ExpiresAtUtc = expiresAtUtc,
            UserName = user.UserName ?? string.Empty,
            Roles = roles
        };

        return response;
    }
}