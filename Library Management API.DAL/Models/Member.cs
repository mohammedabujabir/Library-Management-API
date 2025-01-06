using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Models
{
    public class Member
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email {  get; set; }

        public string MemberShipType {  get; set; }
    }
}
