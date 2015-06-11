using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TODOList.Models
{
    public class TaskContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public DbSet<SubTask> SubTasks { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}