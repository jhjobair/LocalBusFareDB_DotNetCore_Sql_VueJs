namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Interface;
using WebApi.Services;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class FareChartController : ControllerBase
{
    private IFareChartService _fareChartService;
    private IMapper _mapper;

    public FareChartController(
        IFareChartService fareChartService,
        IMapper mapper)
    {
        _fareChartService = fareChartService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var data = _fareChartService.GetAll();
        return Ok(data);
    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var data = _fareChartService.GetById(id);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(FareChart model)
    {
        string res = _fareChartService.Create(model);
        if (res.Length != 0)
        {
            return BadRequest(new { message = res });
        }
        return Ok(new { message = "Stations created" });
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _fareChartService.Delete(id);
        return Ok(new { message = "chartInfo deleted" });
    }
}