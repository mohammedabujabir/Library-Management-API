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
    public class MemberRepository : IMemberRepository
    {
        private readonly string path = "C:\\Users\\Technipal\\source\\repos\\Library Management API\\Library Management API\\Library Management API.DAL\\Data\\members.json";

        public List<Member> GetALLMembers()
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");
            }
            var jsondata = File.ReadAllText(path);
            var members = JsonSerializer.Deserialize<List<Member>>(jsondata, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Log.Information("The members were fetched from the json file successfully");
            return members;
        }

        public void SaveMembers(List<Member> members)
        {
            var jsondata = JsonSerializer.Serialize(members, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
            File.WriteAllText(path, jsondata);
            Log.Information("The members were saved to the json file successfully");
        }
    }
}
