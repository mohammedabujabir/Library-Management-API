using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        
        public string Author {  get; set; }
      
        public string Genre {  get; set; }

        public string ISBN {  get; set; }
    
        public int Quantity {  get; set; }
        public  ICollection<Borrowing> Borrowings {  get; set; }
    }
}
