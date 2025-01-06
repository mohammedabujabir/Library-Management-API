using Library_Management_API.BLL.Services;
using Library_Management_API.BLL.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Library_Management_API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly IReportingService reportingService;

        public ReportingController(IReportingService ReportingService)
        {
            reportingService = ReportingService;
        }


        [HttpGet("GetCurrently")]
        public IActionResult GetCurrentlyBorrowedBooks()
        {
            Log.Information("A request has been sent to create a report on currently borrowed books");
            var report = reportingService.GetCurrentlyBorrowedBooks();
            return Ok(report);
        }

        [HttpGet("GetLateReturns")]
        public IActionResult GetLateReturns()
        {
            Log.Information("A request was sent to create a report on books that were delivered late");
            var report = reportingService.GetLateReturns();
            return Ok(report);
        }

    }
}
