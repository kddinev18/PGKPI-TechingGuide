using System.ComponentModel.DataAnnotations.Schema;

namespace Singer.Persistance.Data.Entities;

public class SingerLabel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Singer> Singers { get; set; }
}