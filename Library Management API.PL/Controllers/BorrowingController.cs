using Library_Management_API.BLL.Services;
using Library_Management_API.BLL.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Library_Management_API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingService borrowingService;

        public BorrowingController(IBorrowingService BorrowingService) 
        {
            borrowingService = BorrowingService;
        }
        [HttpGet]
        public IActionResult GetBorrowing()
        {
            Log.Information("A request has been sent to bring borrowers");
            var borrowings= borrowingService.GetBorrowings();
            return Ok(borrowings);
        }
        [HttpPost("BorrowBook")]
        public IActionResult BorrowBook(int MemberId,int bookId)
        {
            Log.Information("A request has been sent to add the borrowed book");
            borrowingService.Borrowedbook(MemberId, bookId);
            return Ok("book borrowed successfully");
        }

        [HttpPost("ReturnBook")]
        public IActionResult ReturnBook(int MemberId, int bookId) {
            Log.Information("A request has been sent to return the borrowed book");
            borrowingService.ReturnBook(MemberId, bookId);
            return Ok("book returned successfully");
        }
    }
}
