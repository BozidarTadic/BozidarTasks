using System;
using System.Collections.Generic;

#nullable disable

namespace Tasks.Data.Data
{
    public partial class CurrentStatus
    {
        public CurrentStatus()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string CurrentStatus1 { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
