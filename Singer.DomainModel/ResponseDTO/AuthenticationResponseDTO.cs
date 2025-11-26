namespace Singer.DomainModel.ResponseDTO;

public class AuthenticationResponseDTO
{
    public string AccessToken { get; set; }

    public DateTime ExpiresAtUtc { get; set; }

    public string UserName { get; set; } 

    public IEnumerable<string> Roles { get; set; }
}