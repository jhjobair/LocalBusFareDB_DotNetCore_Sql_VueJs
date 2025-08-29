namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Interface;
using WebApi.Services;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class StationsController : ControllerBase
{
    private IStationsService _stationsService;
    private IMapper _mapper;
    private IFareChartService _fareChartService;

    public StationsController(
        IStationsService stationsService,
        IMapper mapper,
        IFareChartService fareChartService)
    {
        _stationsService = stationsService;
        _mapper = mapper;
        _fareChartService = fareChartService;
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
        if (_fareChartService.GetAll().Any(e => e.FromStationId == id || e.ToStationId == id))
        {
            return BadRequest(new { message = "This Chart Info already used in Fare Chart." });

        }
        _stationsService.Delete(id);
        return Ok(new { message = "Stations deleted" });
    }
}