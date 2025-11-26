using Microsoft.AspNetCore.Identity.Data;
using Singer.DomainModel.RequestDTO;
using Singer.DomainModel.ResponseDTO;

namespace Singer.Infrastructure.Services;

public interface IAuthenticationService
{
    public Task Register(RegisterRequestDTO register);
    public Task<AuthenticationResponseDTO> Login(LoginRequestDTO login);
}