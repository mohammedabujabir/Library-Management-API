
using Library_Management_API.DAL.Entities;
using Library_Management_API.DAL.Repositories.IRepositories;
using Mapster;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Repositories.RepositoriesImpl
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IBookRepository bookRepository;

        public BorrowingRepository(ApplicationDbContext dbContext, IBookRepository BookRepository)
        {
            this.dbContext = dbContext;
            bookRepository = BookRepository;
        }
        

        public List<Borrowing> GetBorrowing()
        {
            try
            {
                var borrowers = dbContext.Borrowings.ToList();
                Log.Information("Borrowers from the Database were successfully fetched");
                return borrowers;
            }
            catch (Exception ex) {
                Log.Error($"An error occurred while return the Borrowers from database : {ex.Message}");
                throw new Exception("An error occurred while return the Borrowers from database", ex);
            }
           
        }

        public bool AddBorrowing(int memberId, int bookId)
        {

            try
            {
                var borrowed =GetBorrowing();
                var book = bookRepository.GetBookById(bookId);
                if (book == null || book.Quantity <= 0)
                {
                    Log.Error("The book is not available for borrowing");
                    return false;

                }
                var member = borrowed.Where(m => m.MemberId == memberId && m.ReturnDate == null).ToList();

                if (member.Count() > 4)
                {
                    Log.Error("The member has exceeded the limit allowed for borrowing");
                    return false;

                }

                Borrowing borrowedBook = new Borrowing()
                {
                    MemberId = memberId,
                    BookId = bookId,
                    BorrowDate = DateTime.Now
                };

                book.Quantity--;
                dbContext.Borrowings.Add(borrowedBook);
                dbContext.SaveChanges();
                var bookResult = bookRepository.UpdateBook(book.Id, book);
                Log.Information("The borrower were saved to the database successfully");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to add the borrowed book to the database : {ex.Message}");
                throw new Exception("An error occurred while aadd the borrowed book  to database", ex);
            }

        }

        public bool ReturnBook(int memberId, int bookId)
        {
            try
            {
                var borrowed = GetBorrowing();

                var borrowedBook = borrowed.FirstOrDefault(b => b.MemberId == memberId && b.BookId == bookId);
                if (borrowedBook == null)
                {
                    Log.Error("The book was not borrowed by a person");
                    return false;

                }
                borrowedBook.ReturnDate = DateTime.Now;
                var book = bookRepository.GetBookById(bookId);
                if (book == null)
                {
                    Log.Error("Book Not Found");
                    return false;
                }
                book.Quantity++;
                dbContext.SaveChanges();
                bookRepository.UpdateBook(book.Id, book);
                Log.Information("The borrowed book was successfully returned", book.Id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to return the borrowed book from database : {ex.Message}");
                throw new Exception("An error occurred while return the borrowed book  from database", ex);
            }
        }
    }
}
