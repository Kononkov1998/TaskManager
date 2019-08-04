using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly TaskContext _context;
        [BindProperty]
        public string PreviousStatus { get; set; }

        public EditModel(TaskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Task Task { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Task = await _context.Task.FirstOrDefaultAsync(task => task.ID == id);
            if (Task == null)
            {
                return NotFound();
            }
            PreviousStatus = Task.Status;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var tasks = await _context.Task.AsNoTracking().ToListAsync();
            var task = _context.Task.AsNoTracking().Include(t => t.SubTasks).FirstOrDefault(t => t.ID == Task.ID);

            if (PreviousStatus != Models.Task.possibleStatuses[1] && Task.Status == Models.Task.possibleStatuses[2])
            {
                ModelState.AddModelError("Task.Status", Resources.Strings.PausedError);
            }

            if (PreviousStatus != Models.Task.possibleStatuses[1] && Task.Status == Models.Task.possibleStatuses[3])
            {
                ModelState.AddModelError("Task.Status", Resources.Strings.CompletedError);
            }

            if (PreviousStatus == Models.Task.possibleStatuses[1] && Task.Status == Models.Task.possibleStatuses[3])
            {
                if (!CanBeCompleted(task.SubTasks))
                    ModelState.AddModelError("Task.Status", Resources.Strings.SubTasksCompletedError);
                else
                    CompleteSubTasks(task.SubTasks);
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(Task.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Tasks");
        }

        private void CompleteSubTasks(List<Models.Task> subTasks)
        {
            if (subTasks != null)
            {
                foreach (Models.Task subTask in subTasks)
                {
                    var fullSubTask = _context.Task.Include(t => t.SubTasks).FirstOrDefault(t => t.ID == subTask.ID);
                    fullSubTask.Status = Models.Task.possibleStatuses[3];
                    _context.Attach(fullSubTask).State = EntityState.Modified;
                    CompleteSubTasks(fullSubTask.SubTasks);
                }
            }
        }

        private bool CanBeCompleted(List<Models.Task> subTasks)
        {
            if (subTasks != null)
            {
                foreach (Models.Task subTask in subTasks)
                {
                    var fullSubTask = _context.Task.AsNoTracking().Include(t => t.SubTasks).FirstOrDefault(t => t.ID == subTask.ID);

                    if (fullSubTask.Status != Models.Task.possibleStatuses[1] && fullSubTask.Status != Models.Task.possibleStatuses[3])
                        return false;

                    if (!CanBeCompleted(fullSubTask.SubTasks))
                        return false;
                }
            }
            return true;
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(task => task.ID == id);
        }
    }
}