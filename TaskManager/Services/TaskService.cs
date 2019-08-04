using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        List<Models.Task> ReadAll();
        void Create(Models.Task task, int id);
        Models.Task Read(int id);
        void Update(Models.Task modofiedTask);
        void Delete(int id);
    }
    public class TaskService : ITaskService
    {
        private readonly TaskContext _context;
        public TaskService(TaskContext contex)
        {
            _context = contex;
        }
        public List<Models.Task> ReadAll()
        {
            return _context.Task.ToList();
        }
        public void Create(Models.Task task, int id)
        {
            if (id == 0)            
                _context.Task.Add(task);            
            else
            {
                var mainTask = Read(id);
                mainTask.SubTasks.Add(task);
                _context.Update(mainTask);
            }
            _context.SaveChanges();
        }
        public Models.Task Read(int id)
        {
            return ReadAll().Single(t => t.ID == id);
        }
        public void Update(Models.Task modifiedTask)
        {
            var task = ReadAll().Single(t => t.ID == modifiedTask.ID);
            _context.Update(modifiedTask);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var task = ReadAll().Single(t => t.ID == id);
            foreach(Models.Task subTask in task.SubTasks)
            {
                Delete(subTask.ID);
            }
            _context.Remove(task);
            _context.SaveChanges();
        }
    }
}
