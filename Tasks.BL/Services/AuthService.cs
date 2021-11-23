using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Tasks.Authorization.Token;
using Tasks.BL.Interfaces;
using Tasks.Common;
using Tasks.DAL.Models;
using Tasks.Data;
using Tasks.Data.Data;
using Tasks.Models.Dtos;

namespace Tasks.BL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileService _profileService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private TasksEntities _entity;

        public AuthService(UserManager<ApplicationUser> userManager,
                            IProfileService profileService,
                            SignInManager<ApplicationUser> signInManager,
                            ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _profileService = profileService;
            _tokenService = tokenService;
        }

        public async Task<Response<ProfileDto>> ChangeEmail(string id, AuthDto authDto)
        {
            Response<ProfileDto> response = new Response<ProfileDto>();
            var user = _userManager.FindByIdAsync(id).Result;
            bool passwordCheck = _userManager.CheckPasswordAsync(user, authDto.Password).Result;
            string token = _userManager.GenerateChangeEmailTokenAsync(user, authDto.Email).Result;
            var emailIsUsed = EmailIsUsed(authDto.Email);

            if (emailIsUsed | !passwordCheck)
            {
                if (emailIsUsed)
                {
                    response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                    response.ErrorMessage = Message.EmailIsInUse;
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                    response.ErrorMessage = Message.InvalidCredentials;
                }
            }
            else
            {
                try
                {
                    var result = await _userManager.ChangeEmailAsync(user, authDto.Email, token);
                    await _userManager.SetUserNameAsync(user, authDto.Email);
                    var profile = _profileService.Read(user.ProfileId).Content;

                    var profileDto = new ProfileDto
                    {
                        Email = authDto.Email,
                        Company = profile.Company,
                        Firstname = profile.Firstname,
                        Lastname = profile.Lastname,
                        Gender = profile.Gender,
                        Id = profile.Id,
                        Phone = profile.Phone,
                        StatusId = profile.StatusId
                    };

                    _profileService.Update(profileDto);
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Content = profileDto;

                }
                catch (Exception)
                {

                    response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                }

            }
            return response;
        }

        public async Task<Response<NoValue>> ChangePassword(string id, string currentPassword, string newPassword)
        {
            Response<NoValue> response = new Response<NoValue>();

            var updateUser = _userManager.FindByIdAsync(id).Result;
            try
            {
                var reslut = await _userManager.ChangePasswordAsync(updateUser, currentPassword, newPassword);
                if (reslut.Succeeded)
                {
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                }
            }
            catch (Exception)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public Response<NoValue> Delete(string id, AuthDto authDto)
        {
            Response<NoValue> response = new Response<NoValue>();
            var user = _userManager.FindByIdAsync(id).Result;
            bool passwordCheck = _userManager.CheckPasswordAsync(user, authDto.Password).Result;
            if (passwordCheck && authDto.Email == user.Email)
            {
                response.StatusCode = _profileService.Delete(user.ProfileId).StatusCode;
            }
            else
            {
                response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                response.ErrorMessage = Message.InvalidCredentials;
            }


            return response;
        }

        public Response<string> ForgorPassword(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<ProfileDto>> LoadUser(string id)
        {
            Response<ProfileDto> response = new Response<ProfileDto>();

            var user = await _userManager.FindByIdAsync(id);
            
           var profile = _entity.Profiles.Find(user.ProfileId);

            ProfileDto ret = new ProfileDto()
            {
                Company = profile.Company,
                Email = profile.Email,
                Firstname = profile.Firstname,
                Gender = profile.Gender,
                Id = profile.Id,
                Lastname = profile.Lastname,
                Phone = profile.Phone
            };

            response.StatusCode = System.Net.HttpStatusCode.OK;
            response.Content = ret;
            return response;
        }

        public Response<TokenModel> LogIn(AuthDto authDto)
        {
            Response<TokenModel> response = new Response<TokenModel>();
            //remerber me is currently by default false, could be changed later
            var result = _signInManager.PasswordSignInAsync(authDto.Email, authDto.Password, false, lockoutOnFailure: false).Result;
            var user = _userManager.FindByEmailAsync(authDto.Email).Result;
            bool isDeleted = new bool();
            if (user == null)
            {
                isDeleted = false;
            }
            else
            {
                isDeleted = _profileService.IsDeleted(user.ProfileId);
            }

            if (isDeleted)
            {
                response.ErrorMessage = Message.InvalidCredentials;
                response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
            }
            else
            {
                if (result.Succeeded)
                {
                    var tokensResult = _tokenService.GenerateTokens(user).Result;
                    if (tokensResult.Succeeded)
                    {
                        response.Content = tokensResult.Value;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.StatusCode = System.Net.HttpStatusCode.InternalServerError;

                    }
                }
                else
                {
                    response.ErrorMessage = Message.InvalidCredentials;
                    response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                }
            }


            return response;
        }

        public Response<NoValue> Register(RegistrationDto regDto)
        {
            Response<NoValue> regResponse = new Response<NoValue>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (EmailIsUsed(regDto.Email))
                {
                    regResponse.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                    regResponse.ErrorMessage = Message.EmailIsInUse;
                }
                else
                {
                    ProfileDto profileDto = new ProfileDto()
                    {
                        Company = regDto.Company,
                        Email = regDto.Email,
                        Firstname = regDto.Firstname,
                        Gender = (regDto.Gender == null) ? null : Convert.ToBoolean(regDto.Gender),
                        Lastname = regDto.Lastname,
                        Phone = regDto.Phone
                    };
                    Response<Profile> profileResponse = _profileService.Create(profileDto);

                    if (profileResponse.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        var user = new ApplicationUser { UserName = regDto.Email, Email = regDto.Email, ProfileId = profileResponse.Content.Id };
                        try
                        {
                            var result = _userManager.CreateAsync(user, regDto.Password).Result;

                            if (result.Succeeded)
                            {
                                var res = _userManager.AddToRoleAsync(user, "guest").Result;
                                regResponse.StatusCode = System.Net.HttpStatusCode.OK;
                                scope.Complete();
                            }
                            else
                            {
                                scope.Dispose();
                                regResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                            }
                        }
                        catch (Exception ex)
                        {
                            scope.Dispose();
                            regResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                        }
                    }
                    else
                    {
                        regResponse.StatusCode = profileResponse.StatusCode;
                    }
                }
                return regResponse;
            }
        }

        public Response<NoValue> RestartPassword(RestartPasswordDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<ProfileDto>> Update(ProfileUpdateDto profileDto, string id)
        {
            Response<ProfileDto> updateResponse = new Response<ProfileDto>();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = await _userManager.FindByIdAsync(id);
                var profile = _entity.Profiles.Find(user.ProfileId);

                ProfileDto ret = new ProfileDto()
                {
                    Id = profile.Id,
                    StatusId = profile.StatusId,
                    Email = profile.Email,
                    Gender = profile.Gender,
                    Company = profileDto.Company,
                    Firstname = profileDto.Firstname,
                    Lastname = profileDto.Lastname,
                    Phone = profileDto.Phone
                };


                Response<Profile> profileResponse = _profileService.Update(ret);

                if (profileResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var updateUser = _userManager.FindByIdAsync(id).Result;


                    try
                    {

                        var result = _userManager.UpdateAsync(updateUser).Result;
                        if (result.Succeeded)
                        {
                            //var res = _userManager.AddToRoleAsync(user, "guest").Result;
                            updateResponse.StatusCode = System.Net.HttpStatusCode.OK;
                            updateResponse.Content = ret;
                            scope.Complete();
                        }
                        else
                        {
                            scope.Dispose();
                            updateResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                        }
                    }
                    catch
                    {
                        scope.Dispose();
                        updateResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    }
                }
                else
                {
                    updateResponse.StatusCode = profileResponse.StatusCode;
                }
            }
            return updateResponse;
        }
        public bool EmailIsUsed(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
