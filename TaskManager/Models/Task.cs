using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManager.Models
{
    public class Task
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Perfomers { get; set; }
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        public string Status { get; set; }
        public double PlannedLeadTime { get; set; }
        public double ActualLeadTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime CompletionDate { get; set; }
        public bool IsMainTask { get; set; }
        public List<Task> SubTasks { get; set; }

        public static string[] possibleStatuses = { Resources.Strings.Assigned, Resources.Strings.Perfomed, Resources.Strings.Paused, Resources.Strings.Completed };

        public Task()
        {
            Status = Resources.Strings.Assigned;
            RegistrationDate = DateTime.Now;
            SubTasks = new List<Task>();
        }

        public double GetSubTasksPlannedLeadTime()
        {
            double sum = 0;
            foreach (Task task in SubTasks)
            {
                sum += task.PlannedLeadTime;
            }
            return sum;
        }

        public double GetSubTasksActualLeadTime()
        {
            double sum = 0;
            foreach (Task task in SubTasks)
            {
                sum += task.ActualLeadTime;
            }
            return sum;
        }

        public bool EndTask()
        {
            if (Status == possibleStatuses[1])
            {
                foreach (Task task in SubTasks)
                {
                    if (!task.EndTask())
                        return false;
                }
                Status = possibleStatuses[3];
                return true;
            }
            else
                return false;
        }

        public bool StopTask()
        {
            if (Status == possibleStatuses[1])
            {
                Status = possibleStatuses[2];
                return true;
            }
            else
                return false;
        }

        public void ResumeTask()
        {
            Status = Task.possibleStatuses[1];
        }
    }
}
