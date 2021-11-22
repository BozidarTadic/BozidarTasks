using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.BL.Interfaces;
using Tasks.Models.Dtos;

namespace Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var task = _taskService.GetTasks();

            switch (task.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(task.Content);

                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();

                default:
                    return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var task = _taskService.Gettask(id);

            switch (task.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(task.Content);

                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();

                default:
                    return BadRequest();
            }
        }
        [HttpPost]
        [Route("Update")]
        public IActionResult Update([FromBody] TaskDto dto)
        {
            var task = _taskService.Update(dto);
            switch (task.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(task.Content);

                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();

                default:
                    return BadRequest();
            }
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] TaskDto dto)
        {
            var task = _taskService.create(dto);

            switch (task.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(task.Content);

                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();

                default:
                    return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        private IActionResult Delete(int id)
        {
            var task = _taskService.Delete(id);

            switch (task.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(task.Content);

                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();

                default:
                    return BadRequest();
            }
        }
        [HttpPost]
        [Route("GetByProjectId" )]
        public IActionResult GetByProjectId([FromBody] int id)
        {
            var task = _taskService.getByProjectId(id);
            switch (task.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(task.Content);

                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();

                default:
                    return BadRequest();
            }
        }
    }
}
