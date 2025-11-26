using System.ComponentModel.DataAnnotations;

namespace Singer.DomainModel.RequestDTO;

public sealed class LoginRequestDTO
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}