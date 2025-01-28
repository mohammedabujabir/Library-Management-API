using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.DTOs.Identity
{
    public record UserDto
    {
        public string Token {  get; set; }
    }
}
