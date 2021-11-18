using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Common;
using Tasks.Models.Dtos;

namespace Tasks.BL.Interfaces
{
    public interface IProjectService
    {
        Response<List<ProjectDto>> GetProects();
        Response<ProjectDto> GetProject(int id);
        Response<ProjectDto> Update(ProjectDto projectDto);
        Response<NoValue> Delete(int id);
        Response<ProjectDto> Create(ProjectDto projectDto);

    }
}
