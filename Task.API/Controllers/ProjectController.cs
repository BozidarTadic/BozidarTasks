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
    public class ProjectController : Controller
    {
        private IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
           var project = _projectService.GetProects();

            switch (project.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(project.Content);
                
                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();
                
                default:
                    return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var project = _projectService.GetProject(id);
            switch (project.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(project.Content);
                
                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();
               
                default:
                    return BadRequest();
            }
        }
        [HttpPost]
        [Route("Update")]
        public IActionResult Update([FromBody] ProjectDto projectDto)
        {
            var project = _projectService.Update(projectDto);

            switch (project.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(project.Content);

                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();

                default:
                    return BadRequest();
            }
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] ProjectDto projectDto)
        {
            var project = _projectService.Create(projectDto);

            switch (project.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(project.Content);

                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();

                default:
                    return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _projectService.Delete(id);

            switch (project.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok();

                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();

                default:
                    return BadRequest();
            }
        }

    }
}
