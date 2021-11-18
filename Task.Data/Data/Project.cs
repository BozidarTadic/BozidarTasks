using System;
using System.Collections.Generic;

#nullable disable

namespace Tasks.Data.Data
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int CurrentStatusId { get; set; }
        public int PriorityId { get; set; }

        public virtual CurrentStatus CurrentStatus { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
