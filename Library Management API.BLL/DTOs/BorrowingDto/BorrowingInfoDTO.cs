using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.DTOs.BorrowingDto
{
    public record BorrowingInfoDTO
    {
        public int book_Id { get; set; }
        public string book_Title { get; set; }
        public int memberId { get; set; }
        public string memberName { get; set; }

     

    }
}
