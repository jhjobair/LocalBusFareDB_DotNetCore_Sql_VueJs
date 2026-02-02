//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Reporting.NETCore; // The new namespace
//using WebApi.Interface;
//using System.Data;

//namespace WebApi.Controllers
//{
//    [ApiController]
//    [Route("api/v1/[controller]/[action]")]
//    public class ReportController : ControllerBase
//    {
//        private readonly IFareChartService _fareChartService;
//        private readonly IWebHostEnvironment _env;

//        public ReportController(IFareChartService fareChartService, IWebHostEnvironment env)
//        {
//            _fareChartService = fareChartService;
//            _env = env;
//        }

//        [HttpGet]
//        public IActionResult RDLCReport(string reportType, int fromStationId, int toStationId)
//        {
//            try
//            {
//                // Required for PDF/Excel character encoding support
//                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

//                // 1. Locate the RDLC file
//                // Using WebRootPath ensures we look inside 'wwwroot'
//                string reportPath = Path.Combine(_env.WebRootPath, "Report", "UserReport.rdlc");

//                if (!System.IO.File.Exists(reportPath))
//                {
//                    return NotFound($"Report file not found at: {reportPath}");
//                }

//                // 2. Fetch Data from Service
//                var data = _fareChartService.GetByStations(fromStationId, toStationId);

//                if (data == null)
//                {
//                    return NotFound("No fare chart data found for the selected stations.");
//                }

//                // Create a list for the DataSource (RDLC expects a collection)
//                // Assuming 'data' is a single object, we wrap it in a list. 
//                // If it's already a list, just pass it directly.
//                var dataSourceList = new List<object> { data };

//                // 3. Initialize LocalReport
//                LocalReport report = new LocalReport();
//                report.ReportPath = reportPath;

//                // 4. Add Data Source
//                // IMPORTANT: "DataSet1" must match the name in your UserReport.rdlc exactly!
//                report.DataSources.Add(new ReportDataSource("DataSet1", dataSourceList));

//                // 5. Add Parameters (Optional)
//                ReportParameter[] parameters = new ReportParameter[]
//                {
//                    new ReportParameter("user", "RH Admin")
//                };
//                report.SetParameters(parameters);

//                // 6. Render the report
//                byte[] reportBytes;
//                string mimeType;
//                string fileName;

//                if (reportType.ToLower() == "pdf")
//                {
//                    reportBytes = report.Render("PDF");
//                    mimeType = "application/pdf";
//                    fileName = "FareChartReport.pdf";
//                }
//                else // Excel
//                {
//                    // Use "EXCELOPENXML" for .xlsx or "Excel" for .xls
//                    reportBytes = report.Render("EXCELOPENXML");
//                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//                    fileName = "FareChartReport.xlsx";
//                }

//                // 7. Return the file
//                return File(reportBytes, mimeType, fileName);
//            }
//            catch (Exception ex)
//            {
//                // Helpful error response for debugging
//                return StatusCode(500, new
//                {
//                    message = "An error occurred during report generation.",
//                    error = ex.Message,
//                    inner = ex.InnerException?.Message
//                });
//            }
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using WebApi.Interface;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class ReportController(IFareChartService fareChartService, IWebHostEnvironment env,IStationsService stationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> FullFareChartReport(string reportType)
        {
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                // 1. Path to your RDLC
                string reportPath = Path.Combine(env.WebRootPath, "Report", "FareChartReport.rdlc");

                if (!System.IO.File.Exists(reportPath))
                    return NotFound("Report template not found.");

                // 2. Fetch ALL data from the service
                // Using .GetAll() to get the full table
                var data =  fareChartService.GetAll();

                if (data == null || !data.Any())
                    return NotFound("No data available to generate report.");

                // 3. Initialize Report
                LocalReport report = new LocalReport();
                report.ReportPath = reportPath;

                // 4. Add the full list to the DataSource
                // Note: "DataSet1" must match your RDLC Dataset name exactly
                report.DataSources.Add(new ReportDataSource("DataSet1", data));

                ReportParameter[] parameters = new ReportParameter[]
                    {
                     new ReportParameter("user", "System Admin")
                    };

                // 2. Set the parameters to the report
                report.SetParameters(parameters);
                // 5. Render
                byte[] reportBytes;
                string mimeType;
                string fileName = $"FullFareChart_{DateTime.Now:yyyyMMdd}";

                if (reportType.ToLower() == "excel")
                {
                    reportBytes = report.Render("EXCELOPENXML");
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileName += ".xlsx";
                }
                else
                {
                  
                    
                    reportBytes = report.Render("PDF");
                    mimeType = "application/pdf";
                    fileName += ".pdf";
                }

                return File(reportBytes, mimeType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> StationsReport(string reportType)
        {
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                // 1. Path to your RDLC
                string reportPath = Path.Combine(env.WebRootPath, "Report", "StationsReport.rdlc");

                if (!System.IO.File.Exists(reportPath))
                    return NotFound("Report template not found.");

                // 2. Fetch ALL data from the service
                // Using .GetAll() to get the full table
                var data = stationService.GetAll();

                if (data == null || !data.Any())
                    return NotFound("No data available to generate report.");

                // 3. Initialize Report
                LocalReport report = new LocalReport();
                report.ReportPath = reportPath;

                // 4. Add the full list to the DataSource
                // Note: "DataSet1" must match your RDLC Dataset name exactly
                report.DataSources.Add(new ReportDataSource("stationsDataSet", data));

                ReportParameter[] parameters = new ReportParameter[]
                    {
                     new ReportParameter("CreatedBy", "jobair")
                    };

                // 2. Set the parameters to the report
                report.SetParameters(parameters);
                // 5. Render
                byte[] reportBytes;
                string mimeType;
                string fileName = $"FullFareChart_{DateTime.Now:yyyyMMdd}";

                if (reportType.ToLower() == "excel")
                {
                    reportBytes = report.Render("EXCELOPENXML");
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileName += ".xlsx";
                }
                else
                {


                    reportBytes = report.Render("PDF");
                    mimeType = "application/pdf";
                    fileName += ".pdf";
                }

                return File(reportBytes, mimeType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}