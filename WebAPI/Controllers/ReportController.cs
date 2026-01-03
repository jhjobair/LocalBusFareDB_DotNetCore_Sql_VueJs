using AspNetCore.Reporting;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class ReportController : ControllerBase
    {
        private IFareChartService _fareChartService;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ReportController(IFareChartService fareChartService,IMapper mapper, IWebHostEnvironment env)
        {
            _fareChartService = fareChartService;
            _mapper = mapper;
            _env = env;
        }
        // This is for RDLC Report Controller ----------------------------------------------
       
        /*
        1- create class laibrary project
        2- install extension Microsoft RDLC Report Designer
        3- add new item -> Report -> name it FareChartReport.rdlc
        4- add dataset xsd file to class library project
        5- design your report using dataset fields
        6- install nuget package AspNetCore.Reporting
         */
        [HttpGet]
        public IActionResult RDLCReport( string reportType, int fromStationId, int toStationId)
        {
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

       


                // 2. Get RDLC file path
                string rdlcFilePath = Path.Combine(_env.WebRootPath, "Report", "FareChartReport.rdlc");


                // 3. Prepare parameters (if you had any)
                Dictionary<string, string> parameters = new Dictionary<string, string>()
                {
                    { "user", "RH" }
                };

                // 4. Create LocalReport (AspNetCore.Reporting)
                LocalReport report = new LocalReport(rdlcFilePath);

                // 5. Add Data Sources (names must match RDLC dataset names)
                var data = _fareChartService.GetByStations(fromStationId, toStationId);
                report.AddDataSource("Dataset1", data);

                if (reportType == "pdf") // Click pdf button 
                {
                    // 6. Render PDF
                    var result = report.Execute(RenderType.Pdf, 1, parameters);

                    // 7. Return PDF file
                    return File(result.MainStream, "application/pdf", $"FareChartReport.pdf");

                }
                else
                {
                    // Render Excel
                    var result = report.Execute(RenderType.Excel, 1, parameters);

                    // Return Excel file
                    return File(
                        result.MainStream,
                        "application/vnd.ms-excel",         // Excel 2003 MIME type
                        $"FareChartReport.xls"   // <<< IMPORTANT
                    );

                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }
    }
}
