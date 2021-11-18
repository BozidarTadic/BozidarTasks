using System;
using System.Collections.Generic;

#nullable disable

namespace Tasks.Data.Data
{
    public partial class Status
    {
        public Status()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Status1 { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
