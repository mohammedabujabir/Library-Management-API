using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Repositories.IRepositories
{
    public interface IReportingRepository
    {
        public IEnumerable<dynamic> GetInfoAboutCurrentlyBorrowed();
        public IEnumerable<dynamic> GetInfoAboutLateReturn();
    }
}
