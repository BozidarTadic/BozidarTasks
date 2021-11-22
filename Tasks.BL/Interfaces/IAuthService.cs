using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Authorization.Token;
using Tasks.Common;
using Tasks.Models.Dtos;

namespace Tasks.BL.Interfaces
{
     public interface IAuthService
    {
        public Response<TokenModel> LogIn(AuthDto authDto);
        public Response<NoValue> Register(RegistrationDto regDto);
        public Task<Response<ProfileDto>> LoadUser(string id);
        public Task<Response<ProfileDto>> Update(ProfileUpdateDto profileDto, string id);
        public Task<Response<NoValue>> ChangePassword(string id, string currentPassword, string newPassword);
        public Task<Response<ProfileDto>> ChangeEmail(string id, AuthDto authDto);
        public Response<NoValue> Delete(string id, AuthDto authDto);
        public Response<string> ForgorPassword(string email);
        public Response<NoValue> RestartPassword(RestartPasswordDto dto);
    }
}
