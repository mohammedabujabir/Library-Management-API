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
    public class ReportingService : IReportingService
    {
        private readonly IBorrowingRepository borrowingRepository;
        private readonly IMemberRepository memberRepository;
        private readonly IBookRepository bookRepository;

        public ReportingService(IBorrowingRepository BorrowingRepository, IMemberRepository MemberRepository, IBookRepository BookRepository)
        {
            borrowingRepository = BorrowingRepository;
            memberRepository = MemberRepository;
            bookRepository = BookRepository;
        }

        public List<string> GetCurrentlyBorrowedBooks()
        {
            var borrowings = borrowingRepository.GetAllBorrowing();
            var books = bookRepository.GetAllBooks();
            var members = memberRepository.GetALLMembers();
            var CurrentlyBorrowed = borrowings.Where(b => b.ReturnDate == null).ToList();

            var CurrentlyBorrowedBooks = CurrentlyBorrowed.Select(b =>
            {
                var book = books.FirstOrDefault(item => item.Id == b.BookId);
                var member = members.FirstOrDefault(item => item.Id == b.MemberId);
                if (book != null && member != null)
                {
                    return $"Book: {book.Title},Borrowed by:{member.Name}";
                }
                else
                    return null;
            }).Where(item => item != null).ToList();

            return CurrentlyBorrowedBooks;
        }

        private const int BorrowingPeriod = 5;

        public List<string> GetLateReturns()
        {
            var borrowings = borrowingRepository.GetAllBorrowing();
            var books = bookRepository.GetAllBooks();
            var members = memberRepository.GetALLMembers();
            var lateReturns = borrowings.Where(item => item.ReturnDate == null && item.BorrowDate.AddDays(BorrowingPeriod) < DateTime.Now);
            var lates = lateReturns.Select(b =>
            {
                var book = books.FirstOrDefault(item => item.Id == b.BookId);
                var member = members.FirstOrDefault(item => item.Id == b.MemberId);
                if (book != null && member != null)
                {
                    return $"Book: {book.Title},Borrowed by: {member.Name}";
                }
                else
                    return null;
            }).Where(item => item != null).ToList();
            return lates;

        }
    }
}
