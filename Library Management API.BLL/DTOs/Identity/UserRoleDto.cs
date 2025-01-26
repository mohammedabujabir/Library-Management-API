using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.BLL.DTOs.Identity
{
    public record UserRoleDto
    {
        [Required]
        public string UserName {  get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
