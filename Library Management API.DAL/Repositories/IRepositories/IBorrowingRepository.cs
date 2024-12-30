using Library_Management_API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Repositories.IRepositories
{
    public interface IBorrowingRepository
    {
        List<Borrowing> GetAllBorrowing();
        void SaveBorrowing(List<Borrowing> borrowers);
    }
}
