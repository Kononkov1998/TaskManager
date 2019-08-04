using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Task")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Task
        [HttpGet]
        public IEnumerable<Models.Task> Get()
        {
            return _taskService.ReadAll(); 
        }

        // GET api/Task/5
        [HttpGet("{id}")]
        public Models.Task Get(int id)
        {
            return _taskService.Read(id);
        }

        // POST api/Task/5
        [HttpPost("{id}")]
        public void Post([FromBody]Models.Task task, int id)
        {
            _taskService.Create(task, id);
        }

        // PUT api/Task/5
        [HttpPut]
        public void Put([FromBody]Models.Task task)
        {
            _taskService.Update(task);
        }

        // DELETE api/Task/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _taskService.Delete(id);
        }
    }
}
