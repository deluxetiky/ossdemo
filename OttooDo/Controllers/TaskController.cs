using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OttooDo.Interface.Service;
using OttooDo.Model.Dto;

namespace OttooDo.Controllers
{
    [Produces("application/json")]
    [Route("api/task")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetTasks()
        {
            var tasks = _taskService.GetTasks();
            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTask([FromRoute]string id)
        {
            var task = await _taskService.FindByIdAsync(id);
            return Ok(task);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> InsertTaskAsync([FromBody]TaskElementDto task)
        {
            var res = await _taskService.AddAsync(task);
            return Ok(res);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateTask([FromBody]TaskElementDto task)
        {
            var res = await _taskService.UpdateAsync(task);
            return Ok(res);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute]string id)
        {
            var res = await _taskService.DeleteAsync(id);
            return Ok(res);
        }
    }
}