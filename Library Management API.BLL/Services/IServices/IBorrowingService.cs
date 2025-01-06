using Library_Management_API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.Services.IServices
{
    public interface IBorrowingService
    {
        List<Borrowing> GetBorrowings();
        void Borrowedbook(int MemberId, int BookId);
        void ReturnBook(int MemberId, int BookId);
    }
}
