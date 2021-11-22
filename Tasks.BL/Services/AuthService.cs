using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Authorization.Token;
using Tasks.BL.Interfaces;
using Tasks.Common;
using Tasks.Models.Dtos;

namespace Tasks.BL.Services
{
    public class AuthService : IAuthService
    {
        public Task<Response<ProfileDto>> ChangeEmail(string id, AuthDto authDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoValue>> ChangePassword(string id, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Response<NoValue> Delete(string id, AuthDto authDto)
        {
            throw new NotImplementedException();
        }

        public Response<string> ForgorPassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ProfileDto>> LoadUser(string id)
        {
            throw new NotImplementedException();
        }

        public Response<TokenModel> LogIn(AuthDto authDto)
        {
            throw new NotImplementedException();
        }

        public Response<NoValue> Register(RegistrationDto regDto)
        {
            throw new NotImplementedException();
        }

        public Response<NoValue> RestartPassword(RestartPasswordDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ProfileDto>> Update(ProfileUpdateDto profileDto, string id)
        {
            throw new NotImplementedException();
        }
    }
}
