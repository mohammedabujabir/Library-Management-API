using Library_Management_API.BLL.Services;
using Library_Management_API.BLL.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Library_Management_API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
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
            try
            {
                Log.Information("A request has been sent to create a report on currently borrowed books");
                var report = reportingService.GetCurrentlyBorrowedBooks();
                if (report.Count == 0)
                {
                    return NotFound(report);
                }else
                return Ok(report);
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while preparing a report on currently borrowed books: {ex.Message}");
                return BadRequest("False:Failed to prepare a report on currently borrowed books");
            }
           
        }

        [HttpGet("GetLateReturns")]
        public IActionResult GetLateReturns()
        {
            try
            {
                Log.Information("A request was sent to create a report on books that were delivered late");
                var report = reportingService.GetLateReturns();
                if (report.Count == 0)
                {
                    return NotFound(report);
                }
                else
                    return Ok(report);
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while preparing a report on books that were delivered late: {ex.Message}");
                return BadRequest("False:Failed to prepare a report on books that were delivered late");
            }

        }

    }
}
