using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Helpers
{
    public static class HtmlTasksHelper
    {
        public static string GetTasksTree(List<Models.Task> tasks)
        {
            string htmlOutput = string.Empty;

            if (tasks != null)
            {
                htmlOutput += "<ul id=\"list\">";
                foreach (Models.Task task in tasks.Where(task => task.IsMainTask == true))
                {
                    htmlOutput += "<li id=\"" + task.ID + "\">";
                    htmlOutput += task.Name;
                    htmlOutput += "</li>";
                    htmlOutput += GetSubTasksTree(task.SubTasks);                    
                }
                htmlOutput += "</ul>";
            }
            return htmlOutput;
        }

        public static string GetSubTasksTree(List<Models.Task> tasks)
        {
            string htmlOutput = string.Empty;

            if (tasks != null)
            {
                htmlOutput += "<ul>";
                foreach (Models.Task task in tasks)
                {
                    htmlOutput += "<li id=\"" + task.ID + "\">";
                    htmlOutput += task.Name;
                    htmlOutput += "</li>";
                    htmlOutput += GetSubTasksTree(task.SubTasks);                    
                }
                htmlOutput += "</ul>";
            }
            return htmlOutput;
        }
    }
}
