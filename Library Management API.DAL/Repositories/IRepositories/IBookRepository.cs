
using Library_Management_API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Repositories.IRepositories
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        bool AddBook(Book book);
        Book GetBookById(int id);
        bool DeleteBook(int id);
        bool UpdateBook(int id,Book updateBook);
    }
}
