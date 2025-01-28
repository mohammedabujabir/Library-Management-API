using Library_Management_API.BLL.Services.IServices;

using Library_Management_API.BLL.DTOs.BookDto;
using Library_Management_API.DAL.Entities;
using Library_Management_API.DAL.Repositories;
using Library_Management_API.DAL.Repositories.IRepositories;
using Mapster;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public List<GetBooksDto> GetBooks(string? genre = null, bool? available = null)
        {
            try
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
                var booksDto = books.Adapt<List<GetBooksDto>>();
                return booksDto;
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while return the books: {ex.Message}");
                throw new Exception("An error occurred while return the books", ex);
            }
          
        }
        public bool AddBook(AddBookDto bookDto)
        {
            try
            {
                var book = bookDto.Adapt<Book>();
                var result = bookRepository.AddBook(book);
                if (result)
                    Log.Information("The new book has been added successfully");
                else
                    Log.Error($"Failed to add the new book");
                return result;
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while adding the book: {ex.Message}");
                throw new Exception("An error occurred while adding the book", ex);
            }
            
           
        }
        public bool UpdateBook(int id, UpdateBookDto updatedBookDto)
        {
            try
            {
                var book = updatedBookDto.Adapt<Book>();
                bool result = bookRepository.UpdateBook(id, book);
                if (result)
                    Log.Information("The book data has been updated successfully");
                else
                    Log.Error($"The book with the id {id} does not exist or update failed");
                return result;
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while updating the book: {ex.Message}");
                throw new Exception("An error occurred while updating the book ", ex);
            }
           
               
        }
        public bool DeleteBook(int id)
        {
            try
            {
                var result = bookRepository.DeleteBook(id);
                if (result)
                    Log.Information("The book has been successfully deleted");
                else
                    Log.Error($"Failed to delete book data");
                return result;
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while deleting the book: {ex.Message}");
                throw new Exception("An error occurred while deleting the book", ex);

            }
               
        }
           
        }

    }

