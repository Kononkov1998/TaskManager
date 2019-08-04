using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class TaskContext : DbContext
    {
        public DbSet<Task> Task { get; set; }
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }
    }
}
