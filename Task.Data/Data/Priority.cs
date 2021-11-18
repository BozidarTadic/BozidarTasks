using System;
using System.Collections.Generic;

#nullable disable

namespace Tasks.Data.Data
{
    public partial class Priority
    {
        public Priority()
        {
            Projects = new HashSet<Project>();
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public int Prioritylevel { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
