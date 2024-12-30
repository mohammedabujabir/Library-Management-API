using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title {  get; set; }

        public string Author {  get; set; }

        public string Genre {  get; set; }

        public string ISBN {  get; set; }

        public int Quantity {  get; set; }

    }
}
