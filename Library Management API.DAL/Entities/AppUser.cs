using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Address {  get; set; }
    }
}
