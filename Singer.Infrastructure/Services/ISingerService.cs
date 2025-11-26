using Singer.DomainModel.Base;
using Singer.DomainModel.Filters;
using Singer.DomainModel.RequestDTO;
using Singer.DomainModel.ResponseDTO;

namespace Singer.Infrastructure.Services;

public interface ISingerService
{
    IQueryable<SingerResponseDTO> GetAll(BaseFilter<SingerFilter> filters);
    IQueryable<SingerResponseDTO> Get(int id);
    int Add(SingerRequestDTO singer);
    bool Edit(SingerRequestDTO singer);
    bool Delete(int id);
}