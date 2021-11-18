using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Data
{
    public class TasksEntities : DbContext
    {
        public TasksEntities()
        {
        }

        public TasksEntities(DbContextOptions<TasksEntities> options)
           : base(options)
        {
        }
    }
    
    }
