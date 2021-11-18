using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Models.Dtos
{
    public class ProjectDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public int CurrentstatusId {get; set;}
        public int PriorityId { get; set; }

    }
}
