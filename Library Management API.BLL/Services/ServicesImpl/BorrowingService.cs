using Library_Management_API.BLL.Services.IServices;
using Library_Management_API.BLL.DTOs.BorrowingDto;
using Library_Management_API.DAL.Entities;
using Library_Management_API.DAL.Repositories;
using Library_Management_API.DAL.Repositories.IRepositories;
using Library_Management_API.DAL.Repositories.RepositoriesImpl;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace Library_Management_API.BLL.Services.ServicesImpl
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository borrowingRepository;
       

        public BorrowingService(IBorrowingRepository BorrowingRepository)
        {
            borrowingRepository = BorrowingRepository;
            
        }

        public List<GetBorrowingDto> GetBorrowings()
        {
            try
            {
                var borrowings = borrowingRepository.GetBorrowing();
                var borrowersDto = borrowings.Adapt<List<GetBorrowingDto>>();
                Log.Information("All borrowers were successfully brought");
                return borrowersDto;
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while return the Borrowers: {ex.Message}");
                throw new Exception("An error occurred while return the Borrowers", ex);
            }

        }
        public bool Borrowedbook(int memberId, int bookId)
        {
            try
            {
                var result = borrowingRepository.AddBorrowing(memberId, bookId);
                if (result)
                    Log.Information("The borrowed book has been successfully added");

                else
                    Log.Error("Failed to add the borrowed book");

                return result;
            }
            catch (Exception ex) {
                Log.Error($"Failed to add the borrowed book: {ex.Message}");
                throw new Exception("An error occurred while aadd the borrowed book", ex);
            }
           
                

        }
        public bool ReturnBook(int memberId, int bookId)
        {
            try
            {
                var result = borrowingRepository.ReturnBook(memberId, bookId);
                if (result)
                    Log.Information("The borrowed book was successfully returned");
                else
                    Log.Error($"Failed to return the borrowed book");
                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to return the borrowed book  : {ex.Message}");
                throw new Exception("An error occurred while return the borrowed book", ex);
            }
          
        }

    }
       

   
}
