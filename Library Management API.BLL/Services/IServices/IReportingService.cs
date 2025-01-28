using Library_Management_API.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.Services.IServices
{
    public interface IReportingService
    {
        List<string> GetCurrentlyBorrowedBooks();
        List<string> GetLateReturns();
    }
}
