using Singer.Persistance.Data;

namespace Singer.Infrastructure.Base;

public class BaseService
{
    public BaseService(BaseServiceInjector injector)
    {
        Db = injector.Context;
    }

    public ApplicationDbContext Db { get; set; }
}