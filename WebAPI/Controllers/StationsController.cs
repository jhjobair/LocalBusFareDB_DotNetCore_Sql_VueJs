namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Services;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class StationsController : ControllerBase
{
    private IStationsService _stationsService;
    private IMapper _mapper;

    public StationsController(
        IStationsService stationsService,
        IMapper mapper)
    {
        _stationsService = stationsService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var stations = _stationsService.GetAll();
        return Ok(stations);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var stations = _stationsService.GetById(id);
        return Ok(stations);
    }

    [HttpPost]
    public IActionResult Create(Stations model)
    {
       string res = _stationsService.Create(model);
        if (res.Length!=0)
        {
            return BadRequest(new { message = res });
        }
        return Ok(new { message = "Stations created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Stations model)
    {
        try
        {

            string res = _stationsService.Update(id, model);
            if (res.Length != 0)
            {
                return BadRequest(new { message = res });
            }
            return Ok(new { message = "Stations updated" });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
       
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _stationsService.Delete(id);
        return Ok(new { message = "Stations deleted" });
    }
}