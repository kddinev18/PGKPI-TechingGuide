using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient.DataClassification;

namespace Singer.Persistance.Data.Entities;

public class Singer
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    public decimal Salary { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }

    public int SingerLabelId { get; set; }
    public SingerLabel SingerLabel { get; set; }
}