using Microsoft.EntityFrameworkCore;
using Singer.Persistance.Data;

namespace Singer.Infrastructure.Base;

public class BaseServiceInjector
{
    public BaseServiceInjector(ApplicationDbContext dbContext)
    {
        Context = dbContext;
    }

    public ApplicationDbContext Context { get; set; }
}