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
    [Authorize(Roles ="Admin,User")]
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
            try
            {
                Log.Information("A request has been sent to bring borrowers");
                var borrowings = borrowingService.GetBorrowings();
                if(borrowings.Count()==0)
                    return NotFound(borrowings);
                else
                return Ok(borrowings);
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while fetching borrowed books: {ex.Message}");
                return BadRequest("False:Failed to bring borrowed books");
            }
            
        }
        [HttpPost("BorrowBook")]
        public IActionResult BorrowBook(int memberId,int bookId)
        {
            try
            {
                Log.Information("A request has been sent to add the borrowed book");
                var status= borrowingService.Borrowedbook(memberId, bookId);
                if(status)
                return Ok($"status : {status} book borrowed successfully");
                else return BadRequest($"status: {status} The book is not available for borrowing  or the member exceeded the borrowing limit");
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while adding the borrowed book: {ex.Message}");
                return BadRequest("False:Failed to add the borrowed book");
            }

        }

        [HttpPost("ReturnBook")]
        public IActionResult ReturnBook(int memberId, int bookId) {
            try
            {
                Log.Information("A request has been sent to return the borrowed book");
                var status = borrowingService.ReturnBook(memberId, bookId);
                if(status)
                return Ok($"status : {status} book returned successfully");
                else return BadRequest($"status : {status} The book has not been borrowed by a person or it does not exist ");
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while returning the borrowed book: {ex.Message}");
                return BadRequest("False:Failed to return the borrowed book");
            }
          
        }
    }
}
