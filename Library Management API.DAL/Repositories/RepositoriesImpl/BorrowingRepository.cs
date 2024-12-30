using Library_Management_API.DAL.Models;
using Library_Management_API.DAL.Repositories.IRepositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Repositories.RepositoriesImpl
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly string path = "C:\\Users\\Technipal\\source\\repos\\Library Management API\\Library Management API\\Library Management API.DAL\\Data\\borring.json";

        public List<Borrowing> GetAllBorrowing()
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");
            }
            var jsondata = File.ReadAllText(path);
            var borrowers = JsonSerializer.Deserialize<List<Borrowing>>(jsondata, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Log.Information("Borrowers from the JSON file were successfully fetched");
            return borrowers;

        }

        public void SaveBorrowing(List<Borrowing> borrowers)
        {
            var jsondata = JsonSerializer.Serialize(borrowers, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
            File.WriteAllText(path, jsondata);
            Log.Information("Borrowers were successfully saved in the JSON file");
        }
    }
}
