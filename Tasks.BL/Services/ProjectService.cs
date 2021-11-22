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
    class ProjectService : IProjectService
    {
        private TasksEntities _entity;
        public Response<ProjectDto> Create(ProjectDto projectDto)
        {
            Response<ProjectDto> response = new Response<ProjectDto>();

            Project project = new Project
            {
                Name = projectDto.name,
                StartDate = DateTime.Now,
                CompletionDate = null,
                PriorityId = projectDto.PriorityId,
                CurrentStatusId = 0

            };


            try
            {
               
                _entity.Projects.Update(project);
                response.Content = projectDto;
                response.StatusCode = System.Net.HttpStatusCode.OK;

            }
            catch (Exception)
            {

                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            return response;
        }

        public Response<NoValue> Delete(int id)
        {
            Response<NoValue> response = new Response<NoValue>();

            try
            {
               Project project = _entity.Projects.Where(p => p.Id == id).First();
               if (project == null)
                {
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return response;
                }
               else
                {
                    _entity.Projects.Remove(project);
                    response.StatusCode = System.Net.HttpStatusCode.OK;

                }
            }
            catch (Exception)
            {

                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            return response;
        }

        public Response<List<ProjectDto>> GetProects()
        {
            Response<List<ProjectDto>> response = new Response<List<ProjectDto>>();

            try
            {
                response.Content = _entity.Projects.Select(P => new ProjectDto { 
                id = P.Id,
                name = P.Name,
                StartDate = P.StartDate,
                CompletionDate = (DateTime)P.CompletionDate,
                CurrentstatusId = P.CurrentStatusId,
                PriorityId = P.PriorityId
                }).ToList();
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {

                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            return response;
        }

        public Response<ProjectDto> GetProject(int id)
        {
            Response<ProjectDto> response = new Response<ProjectDto>();

            try
            {
                response.Content = _entity.Projects.Where(p => p.Id == id).Select(x =>new ProjectDto
                {
                    id = x.Id,
                    CompletionDate= (DateTime)x.CompletionDate,
                    name = x.Name,
                    CurrentstatusId = x.CurrentStatusId,
                    StartDate = x.StartDate,
                    PriorityId = x.PriorityId
                }).FirstOrDefault();
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {

                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            return response;
        }

        public Response<ProjectDto> Update(ProjectDto projectDto)
        {
            Response<ProjectDto> response = new Response<ProjectDto>();

            try
            {
                Project project = _entity.Projects.Where(p => p.Id == projectDto.id).FirstOrDefault();

                project.Name = projectDto.name;
                project.PriorityId = projectDto.PriorityId;
                project.CompletionDate = projectDto.CompletionDate;


            }
            catch (Exception)
            {

                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }

            return response;
        }
    }
}
