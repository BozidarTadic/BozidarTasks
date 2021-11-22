using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.BL.Interfaces;
using Tasks.Common;
using Tasks.Data;
using Tasks.Data.Data;
using Tasks.Models.Dtos;

namespace Tasks.BL.Services
{
    public class TaskService : ITaskService
    {
        private TasksEntities _entity;

        public TaskService (TasksEntities entity)
        {
            _entity = entity;
        }
        public Response<TaskDto> create(TaskDto taskDto)
        {
            Response<TaskDto> response = new Response<TaskDto>();

            Data.Data.Task task = new Data.Data.Task
            {
                Name = taskDto.Name,
                Desription = taskDto.Desription,
                StatusId = 0,
                PriorityId = taskDto.PriorityId,
                ProjectId = taskDto.ProjectId
            };

            try
            {
                _entity.Tasks.Add(task);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Content = taskDto;


            }
            catch (Exception)
            {

                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public Response<NoValue> Delete(int id)
        {
            Response<NoValue> response = new Response<NoValue>();

            try
            {
                Data.Data.Task task = _entity.Tasks.Where(t => t.Id == id).FirstOrDefault();

                _entity.Tasks.Remove(task);
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {

                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            return response;
        }

        public Response<List<TaskDto>> getByProjectId(int id)
        {
            Response<List<TaskDto>> response = new Response<List<TaskDto>>();

            try
            {
                response.Content = _entity.Tasks.Where(t => t.ProjectId == id).Select(t => new TaskDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Desription = t.Desription,
                    StatusId = t.StatusId,
                    PriorityId = t.PriorityId,
                    ProjectId = t.ProjectId
                }).ToList();

                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {

                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }

        public Response<TaskDto> Gettask(int id)
        {
            Response<TaskDto> response = new Response<TaskDto>();

            try
            {
                response.Content = _entity.Tasks.Where(t => t.Id == id).Select(p => new TaskDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Desription = p.Desription,
                    PriorityId = p.PriorityId,
                    StatusId = p.StatusId,
                    ProjectId = p.ProjectId
                }).First();

                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {

                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public Response<List<TaskDto>> GetTasks()
        {
            Response<List<TaskDto>> response = new Response<List<TaskDto>>();

            try
            {
                response.Content =_entity.Tasks.Select(t => new TaskDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Desription = t.Desription,
                    PriorityId = t.PriorityId,
                    StatusId = t.StatusId,
                    ProjectId = t.ProjectId
                }).ToList();

                response.StatusCode = System.Net.HttpStatusCode.OK;
            }

            catch (Exception)
            {

                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            return response;
        }

        public Response<TaskDto> Update(TaskDto taskDto)
        {
            Response<TaskDto> response = new Response<TaskDto>();

            Data.Data.Task task = _entity.Tasks.Where(t => t.Id == taskDto.Id).FirstOrDefault();
            if(task == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            task.Name = taskDto.Name;
            task.Desription = taskDto.Desription;
            task.PriorityId = taskDto.PriorityId;
            task.ProjectId = task.ProjectId;
            task.StatusId = task.StatusId;

            try
            {
                _entity.Tasks.Update(task);
                response.Content = taskDto;
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {

                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            return response;
        }
    }
}
