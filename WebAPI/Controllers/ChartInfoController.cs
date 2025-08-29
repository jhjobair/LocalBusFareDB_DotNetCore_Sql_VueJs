namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebApi.Entities;
using WebApi.Interface;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class ChartInfoController : ControllerBase
{
    private IChartInfoService _chartInfoService;
    private IFareChartService _fareChartService;
    private IMapper _mapper;
    private readonly IWebHostEnvironment _hostEnvironment; // 👈 Inject this
    public ChartInfoController(
        IChartInfoService chartInfoService,
        IMapper mapper,
        IWebHostEnvironment hostEnvironment,
        IFareChartService fareChartService)
    {
        _chartInfoService = chartInfoService;
        _mapper = mapper;
        _hostEnvironment = hostEnvironment;
        _fareChartService = fareChartService;
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
    
    private string SaveBase64Image(string base64String)
    {
        // 1. Get the file extension
        var match = Regex.Match(base64String, @"data:image/(?<type>.+?);base64,");
        var fileType = match.Groups["type"].Value;
        var extension = $".{fileType}";

        // 2. Get the pure Base64 data
        var pureBase64 = base64String.Substring(base64String.IndexOf(',') + 1);

        // 3. Decode the Base64 string to a byte array
        var imageBytes = Convert.FromBase64String(pureBase64);

        // 4. Define save path and unique name
        string wwwRootPath = _hostEnvironment.WebRootPath;
        string imagePath = Path.Combine(wwwRootPath, "images", "charts");
        if (!Directory.Exists(imagePath))
        {
            Directory.CreateDirectory(imagePath);
        }
        string uniqueFileName = Guid.NewGuid().ToString() + extension;
        string filePath = Path.Combine(imagePath, uniqueFileName);

        // 5. Save the byte array as a file
        System.IO.File.WriteAllBytes(filePath, imageBytes); // Fixed by explicitly using System.IO.File

        // 6. Return the public-facing URL path
        return $"/images/charts/{uniqueFileName}";
    }
    [HttpPost]
    public IActionResult Create(ChartInfo model)
    {

        // Save the file and get its relative path
        var imagePath = SaveBase64Image(model.ChartPath);
        // IMPORTANT: Overwrite the model's ChartPath with the file URL, not the Base64 string
        model.ChartPath = imagePath;
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
            var imagePath = SaveBase64Image(model.ChartPath);
            // IMPORTANT: Overwrite the model's ChartPath with the file URL, not the Base64 string
            model.ChartPath = imagePath;

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
        if(_fareChartService.GetAll().Any(e=>e.ChartId == id))
        {
            return BadRequest(new { message = "This Chart Info already used in Fare Chart." });

        }
        _chartInfoService.Delete(id);
        return Ok(new { message = "chartInfo deleted" });
    }
}