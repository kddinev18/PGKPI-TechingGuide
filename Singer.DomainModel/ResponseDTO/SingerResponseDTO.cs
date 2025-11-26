using System.Reflection.PortableExecutable;

namespace Singer.DomainModel.ResponseDTO;

public class SingerResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string LabelName { get; set; }
    public decimal Salary { get; set; }
}