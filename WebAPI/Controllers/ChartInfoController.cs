namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Interface;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class ChartInfoController : ControllerBase
{
    private IChartInfoService _chartInfoService;
    private IMapper _mapper;

    public ChartInfoController(
        IChartInfoService chartInfoService,
        IMapper mapper)
    {
        _chartInfoService = chartInfoService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var chartInfo = _chartInfoService.GetAll();
        return Ok(chartInfo);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var chartInfo = _chartInfoService.GetById(id);
        return Ok(chartInfo);
    }

    [HttpPost]
    public IActionResult Create(ChartInfo model)
    {
       string res = _chartInfoService.Create(model);
        if (res.Length!=0)
        {
            return BadRequest(new { message = res });
        }
        return Ok(new { message = "chartInfo created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, ChartInfo model)
    {
        try
        {

            string res = _chartInfoService.Update(id, model);
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
        _chartInfoService.Delete(id);
        return Ok(new { message = "chartInfo deleted" });
    }
}