using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Models.Dtos
{
    public class RestartPasswordDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string newPassword { get; set; }
        [Required]
        public string token { get; set; }
    }
}
