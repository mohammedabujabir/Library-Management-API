using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Entities
{
    public class Member
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [EmailAddress]
        public string Email {  get; set; }

        public string MemberShipType {  get; set; }

        public  ICollection<Borrowing> Borrowings { get; set; }
    }
}
