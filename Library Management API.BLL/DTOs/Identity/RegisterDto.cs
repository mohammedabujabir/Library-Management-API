using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.DTOs.Identity
{
    public record RegisterDto
    {
        [Required]
        public string FullName {  get; set; }
        [Required]
        public string Email {  get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
