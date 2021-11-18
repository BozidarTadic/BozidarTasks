using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Common;
using Tasks.Models.Dtos;

namespace Tasks.BL.Interfaces
{
    public interface ITaskService
    {
        Response<TaskDto> Gettask(int id);
        Response<List<TaskDto>> GetTasks();
        Response<NoValue> Delete(int id);
        Response<TaskDto> Update(TaskDto taskDto);
        Response<TaskDto> create(TaskDto taskDto);
    }
}
