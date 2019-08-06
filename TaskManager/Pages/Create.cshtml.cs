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
                Task.IsMainTask = true;                

                _context.Task.Add(Task);
                
            }
            else
            {
                var mainTask = _context.Task.FirstOrDefault(task => task.ID == id);

                mainTask.SubTasks.Add(Task);
                _context.Task.Update(mainTask);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/tasks");
        }
    }
}