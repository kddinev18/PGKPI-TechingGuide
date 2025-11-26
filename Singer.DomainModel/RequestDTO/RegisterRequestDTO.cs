using System.ComponentModel.DataAnnotations;

namespace Singer.DomainModel.RequestDTO;

public sealed class RegisterRequestDTO
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}