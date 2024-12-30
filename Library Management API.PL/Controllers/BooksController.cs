
using Library_Management_API.BLL.Services;
using Library_Management_API.BLL.Services.IServices;
using Library_Management_API.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Library_Management_API.PL.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
       
        private readonly IBookService bookService;

        public BooksController(IBookService BookService)
        {
            bookService = BookService;
        }

        [HttpGet]
        public IActionResult GetBooks([FromQuery] string? genre, [FromQuery] bool? available)
        {
            Log.Information("A request has been sent to bring books based on genre or available");
            var books = bookService.GetBooks(genre, available);
            return Ok(books);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            Log.Information("A request has been sent to add the new book");
            bookService.AddBook(book);
            return Ok("The book has been added successfully");
        }

        [HttpPut]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            Log.Information("A request has been sent to update the data of the requested book");
            bookService.UpdateBook(id, book);
            return Ok("book data has been successfully modified");
        }

        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            Log.Information("A request has been sent to delete the requested book");
            bookService.DeleteBook(id);
            return Ok("The book has been deleted successfully");
        }


    }
}
