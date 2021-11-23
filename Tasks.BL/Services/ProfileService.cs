using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.BL.Interfaces;
using Tasks.Common;
using Tasks.DAL.Models;
using Tasks.Models.Dtos;

namespace Tasks.BL.Services
{
    public class ProfileService : IProfileService
    {
        public Response<Profile> Create(ProfileDto profile)
        {
            throw new NotImplementedException();
        }

        public Response<NoValue> Delete(long profileId)
        {
            throw new NotImplementedException();
        }

        public bool IsDeleted(long id)
        {
            throw new NotImplementedException();
        }

        public Response<Profile> Read(long profileId)
        {
            throw new NotImplementedException();
        }

        public Response<List<Profile>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Response<Profile> Update(ProfileDto profileDto)
        {
            throw new NotImplementedException();
        }
    }
}
