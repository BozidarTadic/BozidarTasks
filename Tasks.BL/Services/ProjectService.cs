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
            throw new NotImplementedException();
        }

        public Response<NoValue> Delete(int id)
        {
            throw new NotImplementedException();
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

                throw;
            }

            return response;
        }

        public Response<ProjectDto> GetProject(int id)
        {
            throw new NotImplementedException();
        }

        public Response<ProjectDto> Update(ProjectDto projectDto)
        {
            throw new NotImplementedException();
        }
    }
}
