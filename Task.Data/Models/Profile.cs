using System;
using System.Collections.Generic;
using Tasks.Data.Data;

#nullable disable

namespace Tasks.DAL.Models
{
    public partial class Profile
    {
        public Profile()
        {
            AspNetUsers = new HashSet<AspNetUser>();
           
        }

        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool? Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public int StatusId { get; set; }

        public virtual Status Status { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        
    }
}
