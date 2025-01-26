
using Library_Management_API.BLL.Services;
using Library_Management_API.BLL.Services.IServices;
using Library_Management_API.DAL;
using Library_Management_API.BLL.DTOs.BookDto;
using Library_Management_API.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace Library_Management_API.PL.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : Controller
    {
       
        private readonly IBookService bookService;
      

        public BooksController(IBookService BookService )
        {
            bookService = BookService;
            
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetBooks([FromQuery] string? genre, [FromQuery] bool? available)
        {
            try
            {
                Log.Information("A request has been sent to bring books based on genre or available");
                var books = bookService.GetBooks(genre, available);
                if(books.Count()==0)
                    return NotFound(books);
                else
                return Ok(books);
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while fetching books: {ex.Message}");
                return BadRequest("False:Failed to bring books");
            }
           
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBook(AddBookDto book)
        {
            try
            {
                Log.Information("A request has been sent to add the new book");
                var status=bookService.AddBook(book);
                if (status)
                    return Ok($"status: {status} The book has been added successfully");
                else return BadRequest(status);
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while adding the book: {ex.Message}");
                return BadRequest($"False:Failed to add the book");

            }
            
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBook(int id, UpdateBookDto updatedBookDto)
        {
            try
            {
                Log.Information("A request has been sent to update the data of the requested book");
                var status=bookService.UpdateBook(id, updatedBookDto);
                if (status)
                {
                    return Ok($"status {status} abook data has been successfully modified");
                }
                else
                    return BadRequest($"status: {status} Failed to add the book data The book might not exist or the data is invalid");
                
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while updating data for a book: {ex.Message}");
                return BadRequest("False:Failed to update the book data");

            }
           
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                Log.Information("A request has been sent to delete the requested book");
                var status = bookService.DeleteBook(id);
                if (status)
                    return Ok($"status {status} The book has been deleted successfully");
                else
                    return BadRequest($"status: {status} Failed to delete book data  the book does not exist");
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while deleting a book: {ex.Message}");
                return BadRequest("False:Failed to delete the book");
            }
           
        }


    }
}
