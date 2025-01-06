using Library_Management_API.BLL.Services.IServices;
using Library_Management_API.DAL.Models;
using Library_Management_API.DAL.Repositories;
using Library_Management_API.DAL.Repositories.IRepositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.Services.ServicesImpl
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository borrowingRepository;
        private readonly IBookRepository bookRepository;

        public BorrowingService(IBorrowingRepository BorrowingRepository, IBookRepository BookRepository)
        {
            borrowingRepository = BorrowingRepository;
            bookRepository = BookRepository;
        }

        public List<Borrowing> GetBorrowings()
        {
            var borrowings = borrowingRepository.GetAllBorrowing();
            Log.Information("All borrowers were successfully brought");
            return borrowings;
        }
        public void Borrowedbook(int MemberId, int BookId)
        {
            var borrowed = borrowingRepository.GetAllBorrowing();
            var books = bookRepository.GetAllBooks();
            var book = books.FirstOrDefault(b => b.Id == BookId);
            if (book == null || book.Quantity <= 0)
            {
                throw new KeyNotFoundException("The book was not borrowed");

            }
            var member = borrowed.Where(m => m.MemberId == MemberId && m.ReturnDate == null).ToList();
            if (member.Count() > 5)
            {
                throw new KeyNotFoundException("The member has exceeded the limit allowed for borrowing");

            }

            Borrowing BorrowedBook = new Borrowing()
            {
                MemberId = MemberId,
                BookId = BookId,
                BorrowDate = DateTime.Now
            };
            borrowed.Add(BorrowedBook);
            book.Quantity--;

            borrowingRepository.SaveBorrowing(borrowed);
            bookRepository.SaveBooks(books);
            Log.Information("The borrowed book has been successfully added", book.Id);
        }
        public void ReturnBook(int MemberId, int BookId)
        {
            var borrowed = borrowingRepository.GetAllBorrowing();
            var books = bookRepository.GetAllBooks();
            var borrowedbook = borrowed.FirstOrDefault(b => b.MemberId == MemberId && b.BookId == BookId);
            if (borrowedbook == null)
            {
                throw new KeyNotFoundException("He did not borrow any books");

            }
            borrowedbook.ReturnDate = DateTime.Now;
            var book = books.FirstOrDefault(b => b.Id == BookId);
            if (book != null)
            {
                book.Quantity++;
            }
            borrowingRepository.SaveBorrowing(borrowed);
            bookRepository.SaveBooks(books);
            Log.Information("The borrowed book was successfully returned", book.Id);
        }

    }
}
