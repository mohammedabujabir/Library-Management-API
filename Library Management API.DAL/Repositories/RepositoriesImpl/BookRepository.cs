
using Library_Management_API.DAL.Entities;
using Library_Management_API.DAL.Repositories.IRepositories;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Repositories.RepositoriesImpl
{
    public class BookRepository : IBookRepository
    {
       
        private readonly ApplicationDbContext dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        

        public List<Book> GetAllBooks()
        {
            try
            {
                var books = dbContext.Books.ToList();
                Log.Information("The books were fetched from the database successfully");
                return books;

            }
            catch (Exception ex) {
                Log.Error($"An error occurred while return the books from database : {ex.Message}");
                throw new Exception("An error occurred while return the books from database", ex);
            }
           

        }

        public Book GetBookById(int id)
        {
            try
            {
                var book = dbContext.Books.FirstOrDefault(b => b.Id == id);
                if (book is null)
                    Log.Information("Failed to return the book using the ID from the database ");
                else
                    Log.Information("The book was returned using the id from the database successfully ");
                return book;
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while return the book using the ID from database : {ex.Message}");
                throw new Exception("An error occurred while return the book using the ID from database", ex);
            }


        }

        public bool AddBook(Book book)
        {
            try
            {
                dbContext.Books.Add(book);
                dbContext.SaveChanges();
                Log.Information("The book were saved to the database successfully");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while adding the book to database : {ex.Message}");
                throw new Exception("An error occurred while adding the book to database ", ex);
            }
        }

        public bool UpdateBook(int id, Book updateBook)
        {
            try
            {
                var book =GetBookById(id);

                if (book == null)
                {

                    Log.Error($"The book with the identifier {id} does not exist in the database");
                    return false;
                }
                book.Title = updateBook.Title;
                book.Author = updateBook.Author;
                book.Genre = updateBook.Genre;
                book.ISBN = updateBook.ISBN;
                book.Quantity = updateBook.Quantity;
                dbContext.SaveChanges();
                Log.Information("The book data in the database has been updated successfully");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while updating the book in database : {ex.Message}");
                throw new Exception("An error occurred while updating the book in database ", ex);
            }
        }
        public bool DeleteBook(int id)
        {
            try
            {
                var book = GetBookById(id);

                if (book == null)
                {
                    Log.Error($"The book with the identifier {id} does not exist in the database");
                    return false;
                }
                dbContext.Remove(book);
                dbContext.SaveChanges();
                Log.Information("The book was successfully deleted from the database");
                return true;

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while deleting the book from database : {ex.Message}");
                throw new Exception("An error occurred while deleting the book from database  ", ex);
            }
        }
    }
}
