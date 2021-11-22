
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using Tasks.BL.Interfaces;
using Tasks.Common;
using Tasks.Models.Dtos;

namespace Tasks.API.Controllers 
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        

        public AuthController(IAuthService authService )
        {
           _authService = authService;
            
        }
        /// <summary>
        /// login user
        /// </summary>
        /// <remarks>Status 1-Active 2-Delate</remarks>
        /// <param name="authDto"></param>
        /// <returns></returns>
        /// <response code="200">Ok. Returns Security and Refresh Token</response>
        /// <response code="400">Bad request. Invalid parameters.</response>
        /// <response code="401">Invalid credentials</response>
        ///  <response code="500">Unexpected error.</response>
        [HttpPost]
        [Route("Login")]
        public IActionResult LogIn([FromBody] AuthDto authDto)
        {
            var profileResult = _authService.LogIn(authDto);
           
            switch (profileResult.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(profileResult.Content);
                case System.Net.HttpStatusCode.InternalServerError:
                    return StatusCode(500, "Internal server error");
                case System.Net.HttpStatusCode.Unauthorized:
                    return StatusCode(401, profileResult.ErrorMessage.FirstOrDefault()) ;
                default:
                    return  BadRequest();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="regDto"></param>
        /// <returns></returns>
        ///  <response code="200">Ok. Returns Security and Refresh Token</response>
        /// <response code="400">Bad request. Invalid parameters.</response>
        /// <response code="401">Invalid credentials</response>
        ///  <response code="500">Unexpected error.</response>
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] RegistrationDto regDto)
        {
            var regResult = _authService.Register(regDto);

            switch (regResult.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok();
                case System.Net.HttpStatusCode.Unauthorized:
                    return StatusCode(401, regResult.ErrorMessage.FirstOrDefault());
                case System.Net.HttpStatusCode.InternalServerError:
                    return StatusCode(500, "Internal server error");
                default:
                    return BadRequest();
            }
        }
        
        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="profileDto"></param>
        /// <returns></returns>
        /// <response code="200">Ok. return ProfileDto</response
        /// <response code="400">Bad request. Invalid parameters.</response>
        /// <response code="401">Invalid credentials</response>
        /// <response code="500">Unexpected error.</response>
        [HttpPost]
        [Route("UpdateProfile")]
        public IActionResult Update([FromBody] ProfileUpdateDto profileDto)
        {
            //var proileResult = new Task<Response<NoValue>>(null);
           // proileResult.Result.StatusCode = System.Net.HttpStatusCode.BadRequest;
            //if (ModelState.IsValid)
            
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return StatusCode(401, Message.SessionDoesntExist);
            }

            var proileResult = _authService.Update(profileDto, userId);
            
            

            switch (proileResult.Result.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(proileResult.Result.Content);
                case System.Net.HttpStatusCode.InternalServerError:
                    return StatusCode(500, "Internal server error");
                default:
                    return BadRequest();
            }
        }
        /// <summary>
        /// Change Password to User
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <response code="200">Ok.return ProfileDto</response
        /// <response code="400">Bad request. Invalid parameters.</response>
        /// <response code="401">Invalid credentials</response>
        /// <response code="500">Unexpected error.</response>
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword([FromBody] PasswordDto password)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return StatusCode(401, Message.SessionDoesntExist);
            }
            // string token = User.
            var proileResult = _authService.ChangePassword(userId, password.password, password.newPassword);

            switch (proileResult.Result.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok();
                case System.Net.HttpStatusCode.InternalServerError:
                    return StatusCode(500, "Internal server error");
                case System.Net.HttpStatusCode.Unauthorized:
                    return StatusCode(401, proileResult.Result.ErrorMessage.FirstOrDefault());
                default:
                    return BadRequest();
            }
        }
        /// <summary>
        /// Change email
        /// </summary>
        /// <param name="authDto"></param>
        /// <returns></returns>
        /// <response code="200">Ok.return ProfileDto</response
        /// <response code="400">Bad request. Invalid parameters.</response>
        /// <response code="401">Invalid credentials</response>
        /// <response code="500">Unexpected error.</response>
        [HttpPost]
        [Route("ChangeEmail")]
        public IActionResult ChangeEmail([FromBody] AuthDto authDto)
        {
            //User.Identities.
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return StatusCode(401, Message.SessionDoesntExist);
            }
            var proileResult = _authService.ChangeEmail(userId, authDto);


            switch (proileResult.Result.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(proileResult.Result.Content);
                case System.Net.HttpStatusCode.InternalServerError:
                    return StatusCode(500, "Internal server error");
                case System.Net.HttpStatusCode.Unauthorized:
                    return StatusCode(401, proileResult.Result.ErrorMessage.FirstOrDefault());
                default:
                    return BadRequest();
            }
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="authDto"></param>
        /// <returns></returns>
        /// <response code="200">Ok.</response
        /// <response code="400">Bad request. Invalid parameters.</response>
        /// <response code="401">Invalid credentials</response>
        /// <response code="500">Unexpected error.</response>
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete([FromBody] AuthDto authDto)
        {
            
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = _authService.Delete(userId, authDto);
            if (userId == null)
            {
                return StatusCode(401, Message.SessionDoesntExist);
            }

            switch (result.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok();
                case System.Net.HttpStatusCode.InternalServerError:
                    return StatusCode(500, "Internal server error");
                case System.Net.HttpStatusCode.Unauthorized:
                    return StatusCode(401, result.ErrorMessage.FirstOrDefault());
                default:
                    return BadRequest();
            }

        }
        /// <summary>
        /// Load user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ok.return ProfileDto</response
        /// <response code="400">Bad request. Invalid parameters.</response>
        /// <response code="500">Unexpected error.</response>
        [HttpGet]
        [Route("LoadUser")]
        public IActionResult LoadUser()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return StatusCode(401, Message.SessionDoesntExist);
            }

            var ret = _authService.LoadUser(userId);

            switch (ret.Result.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(ret.Result.Content);
                case System.Net.HttpStatusCode.InternalServerError:
                    return StatusCode(500, "Internal server error");
                default:
                    return BadRequest();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ForgorPassword")]
        public IActionResult ForgorPassword(string email)
        {
            

            var ret = _authService.ForgorPassword(email);

            switch (ret.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(ret.Content);
                case System.Net.HttpStatusCode.InternalServerError:
                    return StatusCode(500, "Internal server error");
                case System.Net.HttpStatusCode.NotFound:
                    return StatusCode(404, ret.ErrorMessage.FirstOrDefault());
                default:
                    return BadRequest();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RestartPassword")]
        public IActionResult RestartPassword(RestartPasswordDto dto)
        {


            var ret = _authService.RestartPassword(dto);

            switch (ret.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(ret.Content);
                case System.Net.HttpStatusCode.InternalServerError:
                    return StatusCode(500, "Internal server error");
                case System.Net.HttpStatusCode.FailedDependency:
                    return StatusCode(424, ret.ErrorMessage.FirstOrDefault());
                default:
                    return BadRequest();
            }
        }
    }
}
