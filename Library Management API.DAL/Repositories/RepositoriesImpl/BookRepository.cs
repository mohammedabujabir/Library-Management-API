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
    public class BookRepository : IBookRepository
    {
        private readonly string path = "C:\\Users\\Technipal\\source\\repos\\Library Management API\\Library Management API\\Library Management API.DAL\\Data\\books.json";



        public List<Book> GetAllBooks()
        {

            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");
            }

            var jsonData = File.ReadAllText(path);
            var books = JsonSerializer.Deserialize<List<Book>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Log.Information("The books were fetched from the json file successfully");
            return books;

        }


        public void SaveBooks(List<Book> books)
        {
            var jsonData = JsonSerializer.Serialize(books, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(path, jsonData);
            Log.Information("The books were saved to the json file successfully");
        }


    }
}
