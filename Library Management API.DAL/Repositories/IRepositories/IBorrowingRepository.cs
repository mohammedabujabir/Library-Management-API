
using Library_Management_API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Repositories.IRepositories
{
    public interface IBorrowingRepository
    {
        List<Borrowing> GetBorrowing();
        bool AddBorrowing(int memberId, int bookId);
        bool ReturnBook(int memberId, int bookId);
    }
}
