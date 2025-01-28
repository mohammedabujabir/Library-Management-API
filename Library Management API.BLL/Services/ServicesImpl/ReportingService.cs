using Library_Management_API.BLL.DTOs.BookDto;
using Library_Management_API.BLL.DTOs.BorrowingDto;
using Library_Management_API.BLL.Services.IServices;
using Library_Management_API.DAL;
using Library_Management_API.DAL.Entities;
using Library_Management_API.DAL.Repositories;
using Library_Management_API.DAL.Repositories.IRepositories;
using Library_Management_API.DAL.Repositories.RepositoriesImpl;
using Mapster;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library_Management_API.BLL.Services.ServicesImpl
{
    public class ReportingService : IReportingService
    {
        private readonly IReportingRepository reportingRepository;

        public ReportingService(IReportingRepository reportingRepository)
        {
            this.reportingRepository = reportingRepository;
        }

        public List<string> GetCurrentlyBorrowedBooks()
        {
            try
            {               
                var result= reportingRepository.GetInfoAboutCurrentlyBorrowed();
                var resultAdept = result.Adapt<IEnumerable<BorrowingInfoDTO>>();
                var currentlyBorrowedBooks = resultAdept.Select(item =>
                {
                    if (item != null)
                    {
                        return $"Book: {item.book_Title},Borrowed by:{item.memberName}";
                    }

                    else
                    {
                        return "GetCurrentlyBorrowedBooks Data unavailable";
                    };
                }).ToList();
                Log.Information("A report has been prepared on the currently borrowed books");
                return currentlyBorrowedBooks;
            }catch(Exception ex)
            {
                Log.Error("An error occurred while preparing a report on currently borrowed books", ex.Message);
                throw new Exception("An error occurred while preparing a report on currently borrowed books", ex);
            }
        }
        public List<string> GetLateReturns()
        {
            try
            {
                var result = reportingRepository.GetInfoAboutLateReturn();
                var resultAdept = result.Adapt<IEnumerable<BorrowingInfoDTO>>();
                var lates = resultAdept.Select(item =>
                {
                    if (item != null)
                    {
                        return $"Book: {item.book_Title},Borrowed by: {item.memberName}";
                    }
                    else
                    {
                        return "GetLateReturns Data unavailable";
                    };
                }).ToList();
                Log.Information("A report was prepared on the books that were delivered late");
                return lates;

            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while preparing a report on books that were delivered late", ex.Message);
                throw new Exception("An error occurred while preparing a report on books that were delivered late", ex);
            }
        }
    }
}

