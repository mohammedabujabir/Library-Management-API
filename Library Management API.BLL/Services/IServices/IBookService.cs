using Library_Management_API.DAL.Models;
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
        void AddBook(Book book);
        void UpdateBook(int id, Book updatedBook);
        List<Book> GetBooks(string? genre = null, bool? available = null);
        void DeleteBook(int id);
    }
}
