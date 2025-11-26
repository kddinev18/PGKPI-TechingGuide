using Singer.DomainModel.Base;
using Singer.DomainModel.Filters;
using Singer.DomainModel.RequestDTO;
using Singer.Infrastructure.Services;

namespace Singer.Controller;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]/[action]")]
public class SingerController : Controller
{
    private readonly ISingerService _singerService;
    public SingerController(ISingerService singerService)
    {
        _singerService = singerService;
    }


    [HttpPost]
    public IActionResult GetAll([FromBody] BaseFilter<SingerFilter> filters)
    {
        return Ok(_singerService.GetAll(filters));
    }

    [HttpGet]
    public IActionResult Get([FromQuery] int id)
    {
        return Ok(_singerService.Get(id));
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] SingerRequestDTO singer)
    {
        return Ok(_singerService.Add(singer));
    }
    
    [HttpPatch]
    public IActionResult Edit([FromBody] SingerRequestDTO singer)
    {
        return Ok(_singerService.Edit(singer));
    }
    
    [HttpDelete]
    public IActionResult Delete([FromQuery] int id)
    {
        return Ok(_singerService.Delete(id));
    }
}