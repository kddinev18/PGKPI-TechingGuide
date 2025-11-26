using Singer.DomainModel.Base;
using Singer.DomainModel.Filters;
using Singer.DomainModel.RequestDTO;
using Singer.DomainModel.ResponseDTO;
using Singer.Infrastructure.Base;
using Singer.Infrastructure.Services;

namespace Singer.BusinessLogic.Services;

public class SingerService : BaseService, ISingerService
{
    public SingerService(BaseServiceInjector injector) : base(injector)
    {
    }

    public IQueryable<SingerResponseDTO> GetAll(BaseFilter<SingerFilter> filters)
    {
        if (string.IsNullOrEmpty(filters.FreeTextSearch))
        {
            return ApplyMapping(ApplyPagination(ApplyFilters(GetAllFromDatabase(), filters.Filters), filters.Page, filters.PageSize));
        }
        return ApplyMapping(ApplyPagination(ApplyFreeTextSearch(GetAllFromDatabase(), filters.FreeTextSearch), filters.Page, filters.PageSize));
    }

    public IQueryable<SingerResponseDTO> Get(int id)
    {
        return ApplyMapping(GetAllFromDatabase().Where(s => s.Id == id));
    }

    public int Add(SingerRequestDTO singer)
    {
        Persistance.Data.Entities.Singer entity = new Persistance.Data.Entities.Singer()
        {
            Email = singer.Email,
            Address = singer.Address,
            Name = singer.Name,
            Salary = singer.Salary,
            SingerLabelId = singer.LabelId
        };
        
        Db.Singers.Add(entity);

        Db.SaveChanges();
        
        return entity.Id;
    }

    public bool Edit(SingerRequestDTO singer)
    {
        Persistance.Data.Entities.Singer entity = GetAllFromDatabase()
            .Where(s => s.Id == singer.Id)
            .Single();
        
        entity.Email = singer.Email;
        entity.Address = singer.Address;
        entity.Name = singer.Name;
        entity.Salary = singer.Salary;
        entity.SingerLabelId = singer.LabelId;
        
        return Db.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        Db.Singers.Remove(GetAllFromDatabase().Where(s => s.Id == id).Single());
        
        return Db.SaveChanges() > 0;
    }

    private IQueryable<Persistance.Data.Entities.Singer> ApplyPagination(IQueryable<Persistance.Data.Entities.Singer> query, int page, int pageSize)
    {
        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }

    private IQueryable<Persistance.Data.Entities.Singer> ApplyFreeTextSearch(IQueryable<Persistance.Data.Entities.Singer> query, string text)
    {
        return query.Where(x => x.Name.Contains(text) || x.Email.Contains(text) || x.Address.Contains(text));
    }
    
    private IQueryable<SingerResponseDTO> ApplyMapping(IQueryable<Persistance.Data.Entities.Singer> query)
    {
        return (from singer in query
            join label in Db.SingerLabels on singer.SingerLabelId equals label.Id
            select new SingerResponseDTO()
            {
                Id = singer.Id,
                Name = singer.Name,
                Salary = singer.Salary,
                Email = singer.Email,
                Address = singer.Address,
                LabelName = label.Name,
            });
    }

    private IQueryable<Persistance.Data.Entities.Singer> ApplyFilters(IQueryable<Persistance.Data.Entities.Singer> query, SingerFilter? filters)
    {
        if (filters == null)
        {
            return query;
        }

        if (!string.IsNullOrEmpty(filters.Name))
        {
            query = query.Where(s => s.Name == filters.Name);
        }

        if (!string.IsNullOrEmpty(filters.Email))
        {
            query = query.Where(s => s.Email == filters.Email);
        }

        if (!string.IsNullOrEmpty(filters.Address))
        {
            query = query.Where(s => s.Address == filters.Address);
        }
        
        return query;
    }
    private IQueryable<Persistance.Data.Entities.Singer> GetAllFromDatabase()
    {
        return Db.Singers.AsQueryable();
    }
}