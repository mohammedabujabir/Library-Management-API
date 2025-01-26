using Library_Management_API.BLL.DTOs.BorrowingDto;
using Library_Management_API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.Services.IServices
{
    public interface IBorrowingService
    {
        List<GetBorrowingDto> GetBorrowings();
        bool Borrowedbook(int memberId, int bookId);
        bool ReturnBook(int memberId, int bookId);
    }
}
