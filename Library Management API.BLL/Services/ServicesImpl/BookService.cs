using Library_Management_API.BLL.Services.IServices;
using Library_Management_API.DAL.Models;
using Library_Management_API.DAL.Repositories;
using Library_Management_API.DAL.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.Services.ServicesImpl
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository BookRepository)
        {
            bookRepository = BookRepository;
        }

        public List<Book> GetBooks(string? genre = null, bool? available = null)
        {
            var books = bookRepository.GetAllBooks();


            if (!string.IsNullOrEmpty(genre))
            {
                books = books.Where(b => b.Genre.Equals(genre)).ToList();
                Log.Information("The data was returned based on the genre");
            }


            if (available != null)
            {
                books = books.Where(b => available == true ? b.Quantity > 0 : b.Quantity == 0).ToList();
                Log.Information("The data was returned based on the available");
            }

            return books;
        }
        public void AddBook(Book book)
        {
            var books = bookRepository.GetAllBooks();


            book.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;

            books.Add(book);

            bookRepository.SaveBooks(books);
            Log.Information("The new book has been added successfully", book.Id, book.Title);
        }
        public void UpdateBook(int id, Book updatedBook)
        {
            var books = bookRepository.GetAllBooks();
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }


            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.ISBN = updatedBook.ISBN;
            book.Quantity = updatedBook.Quantity;

            bookRepository.SaveBooks(books);
            Log.Information("The book data has been updated successfully", book.Id);
        }
        public void DeleteBook(int id)
        {
            var books = bookRepository.GetAllBooks();
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            books.Remove(book);
            bookRepository.SaveBooks(books);
            Log.Information("The book has been successfully deleted", book.Id, book.Title);
        }

    }
}
