using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Models;

namespace TaskManager.Pages
{
    public class CreateModel : PageModel
    {
        private readonly TaskContext _context;
        [BindProperty]
        public Models.Task Task { get; set; }
        public CreateModel(TaskContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id == null)
            {
                Task.RegistrationDate = DateTime.Now;
                Task.ActualLeadTime = 0;
                Task.Status = Resources.Strings.Assigned;
                Task.SubTasks = new List<Models.Task>();

                _context.Task.Add(Task);
                
            }
            else
            {
                var mainTask = _context.Task.Single(task => task.ID == id);
                Task.RegistrationDate = DateTime.Now;
                Task.ActualLeadTime = 0;
                Task.Status = Resources.Strings.Assigned;
                Task.SubTasks = new List<Models.Task>();

                mainTask.SubTasks.Add(Task);
                _context.Task.Update(mainTask);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/tasks");
        }
    }
}