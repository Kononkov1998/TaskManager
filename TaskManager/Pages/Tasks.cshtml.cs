using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Pages
{
    public class TasksModel : PageModel
    {
        private readonly TaskContext _context;
        public List<Task> Tasks { get; set; }
        public Task CurrentTask { get; set; }
        public TasksModel(TaskContext contex)
        {
            _context = contex;
        }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
                Tasks = await _context.Task.ToListAsync();
        }

        public double GetSubTasksPlannedLeadTime()
        {
            double sum = 0;
            foreach (Task task in CurrentTask.SubTasks)
            {
                sum += task.PlannedLeadTime;
            }
            return sum;
        }

        public double GetSubTasksActualLeadTime()
        {
            double sum = 0;
            foreach (Task task in CurrentTask.SubTasks)
            {
                sum += task.ActualLeadTime;
            }
            return sum;
        }

        public bool StopTask()
        {
            if (CurrentTask.Status == Task.possibleStatuses[1])
            {
                CurrentTask.Status = Task.possibleStatuses[2];
                return true;
            }
            else
                return false;
        }

        public void ResumeTask()
        {
            CurrentTask.Status = Task.possibleStatuses[1];
        }
    }
}