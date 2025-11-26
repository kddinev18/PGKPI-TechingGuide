using Singer.DomainModel.Base;

namespace Singer.DomainModel.Filters;

public class SingerFilter : IFilter
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
}