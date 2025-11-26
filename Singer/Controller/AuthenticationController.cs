using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Singer.DomainModel.RequestDTO;
using Singer.DomainModel.ResponseDTO;
using Singer.Infrastructure.Services;
using Singer.Persistance.Data.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Singer.Controller;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
    {
        try
        {
            await _authenticationService.Register(registerRequestDTO);
            return Created();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
    {
        try
        {
            return Ok(await _authenticationService.Login(loginRequestDTO));
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}