using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Entities
{
    public class Borrowing
    {
       
        public int MemberId {  get; set; }
        public   Member Member { get; set; }
        public int BookId {  get; set; }
        public  Book Book { get; set; }
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
