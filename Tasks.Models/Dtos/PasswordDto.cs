using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Models.Dtos
{
    public class PasswordDto
    {
        [Required]
        public string password { get; set; }
        [Required]
        public string newPassword { get; set; }
    }
}
