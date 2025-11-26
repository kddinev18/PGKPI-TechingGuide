using System.ComponentModel.DataAnnotations;

namespace Singer.DomainModel.RequestDTO;

public class SingerRequestDTO
{
    public int? Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public decimal Salary { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public int LabelId { get; set; }
}