
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Common;
using Tasks.DAL.Models;
using Tasks.Models.Dtos;

namespace Tasks.BL.Interfaces
{
    public interface IProfileService
    {
        public Response<Profile> Create(ProfileDto profile);
        public Response<Profile> Read(long profileId);
        public Response<List<Profile>> ReadAll();
        public Response<Profile> Update(ProfileDto profileDto);
        public Response<NoValue> Delete(long profileId);
        public bool IsDeleted(long id);
        
    }
}
