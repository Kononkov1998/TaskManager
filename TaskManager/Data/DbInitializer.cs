using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = new TaskContext(serviceProvider.GetRequiredService<DbContextOptions<TaskContext>>());

            // Look for any students.
            if (context.Task.Any())
            {
                return;   // DB has been seeded
            }

            var tasks = new Task[]
            {
            new Task{
                Name ="Carson", Description="Alexander", Perfomers="person1, person2",
                PlannedLeadTime =0.55,
                ActualLeadTime =0.78,
                CompletionDate =DateTime.Parse("2005-10-01"),
                SubTasks = new List<Task> {
                    new Task{Name="Meredith", Description="Alonso", Perfomers="person1, person2", PlannedLeadTime=0.55, ActualLeadTime=0.78, CompletionDate=DateTime.Parse("2005-10-01") },
                    new Task{Name="Arturo", Description="Anand", Perfomers="person1, person2", PlannedLeadTime=0.55, ActualLeadTime=0.78, CompletionDate=DateTime.Parse("2005-10-01")
                    } },
            IsMainTask = true},
            new Task{Name="Carson2", Description="Alexander2", Perfomers="person1, person2", PlannedLeadTime=0.55, ActualLeadTime=0.78, CompletionDate=DateTime.Parse("2005-10-01"), SubTasks= new List<Task> {
                    new Task{Name="Meredith", Description="Alonso", Perfomers="person1, person2", PlannedLeadTime=0.55, ActualLeadTime=0.78, CompletionDate=DateTime.Parse("2005-10-01") } },
            IsMainTask = true},
            new Task{Name="Carson3", Description="Alexander3", Perfomers="person1, person2", PlannedLeadTime=0.55, ActualLeadTime=0.78, CompletionDate=DateTime.Parse("2005-10-01"), IsMainTask = true },

            new Task
            {
                Name = "Carson55",
                Description = "Alexander55",
                Perfomers = "someone",
                PlannedLeadTime = 0.55,
                ActualLeadTime = 0.78,
                CompletionDate = DateTime.Parse("2005-10-01"),
                IsMainTask = true
            }};

            foreach (Task t in tasks)
            {
                context.Task.Add(t);
            }
            context.SaveChanges();
        }
    }
}
