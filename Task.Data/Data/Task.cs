using System;
using System.Collections.Generic;

#nullable disable

namespace Tasks.Data.Data
{
    public partial class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desription { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public int ProjectId { get; set; }

        public virtual Priority Priority { get; set; }
        public virtual Project Project { get; set; }
        public virtual Status Status { get; set; }
    }
}
