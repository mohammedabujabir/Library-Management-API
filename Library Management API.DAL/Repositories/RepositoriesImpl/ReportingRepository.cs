using Library_Management_API.DAL.Repositories.IRepositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Repositories.RepositoriesImpl
{
    public class ReportingRepository: IReportingRepository
    {
        private readonly IBorrowingRepository borrowingRepository;
        private readonly IMemberRepository memberRepository;
        private readonly IBookRepository bookRepository;
        private readonly ApplicationDbContext dbContext;
        private readonly int borrowingPeriod;
        public ReportingRepository(IBorrowingRepository BorrowingRepository, IMemberRepository MemberRepository, IBookRepository BookRepository, ApplicationDbContext dbContext)
        {
            borrowingRepository = BorrowingRepository;
            memberRepository = MemberRepository;
            bookRepository = BookRepository;
            this.dbContext = dbContext;
            borrowingPeriod = 5;
        }

        public IEnumerable<dynamic> GetInfoAboutCurrentlyBorrowed()
        {
            try
            {
                var borrowings = borrowingRepository.GetBorrowing();
                var currentlyBorrowed = borrowings.Where(b => b.ReturnDate == null).ToList();
                var result = currentlyBorrowed.Join(dbContext.Books,
                    item => item.BookId,
                    book => book.Id,
                    (item, book) => new
                    {
                        bookId = book.Id,
                        bookTitle = book.Title,
                        memberId = item.MemberId,
                    }
                    ).Join(dbContext.Members,
                    item => item.memberId,
                    member => member.Id,
                    (item, member) => new
                    {
                        book_Id = item.bookId,
                        book_Title = item.bookTitle,
                        memberId = member.Id,
                        memberName = member.Name,

                    }
                    );
                Log.Information("A report has been prepared on the currently borrowed books");
                return result;
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while reporting books currently on loan in the repository", ex.Message);
                throw new Exception("An error occurred while reporting books currently on loan in the repository", ex);
            }

        }
        public IEnumerable<dynamic> GetInfoAboutLateReturn()
        {
            try
            {
                var borrowings = borrowingRepository.GetBorrowing();
                var lateReturns = borrowings.Where(item => item.ReturnDate == null && item.BorrowDate.AddDays(borrowingPeriod) < DateTime.Now);
                var result = lateReturns.Join(dbContext.Books,
                    item => item.BookId,
                    book => book.Id,
                    (item, book) => new
                    {
                        bookId = book.Id,
                        bookTitle = book.Title,
                        memberId = item.MemberId,
                    }
                    ).Join(dbContext.Members,
                    item => item.memberId,
                    member => member.Id,
                    (item, member) => new
                    {
                        book_Id = item.bookId,
                        book_Title = item.bookTitle,
                        memberId = member.Id,
                        memberName = member.Name,

                    }
               );
                      Log.Information("A report was prepared on the books that were delivered late");
                      return result;
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while preparing a report on books that were delivered latein the repository", ex.Message);
                throw new Exception("An error occurred while preparing a report on books that were delivered latein the repository", ex);
            }


        }
    }
}
