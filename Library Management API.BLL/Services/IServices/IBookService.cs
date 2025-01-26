
using Library_Management_API.BLL.DTOs.BookDto;
using Library_Management_API.DAL.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.Services.IServices
{
    public interface IBookService
    {
        bool AddBook(AddBookDto bookDto);
        bool UpdateBook(int id, UpdateBookDto updatedBookDto);
        List<GetBooksDto> GetBooks(string? genre = null, bool? available = null);
        bool DeleteBook(int id);
    }
}
