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
    class TaskService : ITaskService
    {
        private TasksEntities _entity;

        public TaskService (TasksEntities entity)
        {
            _entity = entity;
        }
        public Response<TaskDto> create(TaskDto taskDto)
        {
            throw new NotImplementedException();
        }

        public Response<NoValue> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Response<TaskDto> Gettask(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<TaskDto>> GetTasks()
        {
            throw new NotImplementedException();
        }

        public Response<TaskDto> Update(TaskDto taskDto)
        {
            throw new NotImplementedException();
        }
    }
}
