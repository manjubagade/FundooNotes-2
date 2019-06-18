// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace FundooNotesBackEnd.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using System.Messaging;
    using BusinessLayer.Interfaces;
    using Experimental.System.Messaging;
    using FundooApi;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class UserController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ////[Route("ConfirmEmail", Name = "ConfirmEmail")]
        ////[AllowAnonymous]
        ////[HttpGet]
        ////public async Task<IActionResult> ConfirmEmail(string userName, string code)
        ////{

        ////    var user = await this.userManager.FindByNameAsync(userName);
        ////    if (user == null || code == null)
        ////    {
        ////        return this.NotFound();
        ////    }

        ////    var result = await this.userManager.ConfirmEmailAsync(user, code);
        ////    return this.Ok(result.Succeeded ? "ConfirmEmail" : "Error");
        ////}

        ///// <summary>
        ///// Logins the specified control.
        ///// </summary>
        ///// <param name="control">The control.</param>
        ///// <returns>The IActionResult</returns>
        ////[HttpPost]
        ////[Route("login")]
        ////public async Task<IActionResult> Login(LoginControl control)
        ////{
        ////    ////Checking Name
        ////    var user = await this.userManager.FindByNameAsync(control.UserName);
        //// Checking Name And Password
        ////    if (user != null && await this.userManager.CheckPasswordAsync(user, control.Password))
        ////    {
        ////        var tokenDesciptor = new SecurityTokenDescriptor
        ////        {
        ////            Subject = new ClaimsIdentity(new Claim[]
        ////            {
        ////                new Claim("UserId", user.Id.ToString())
        ////           }),
        ////            Expires = DateTime.UtcNow.AddDays(1),
        ////            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
        ////        };
        ////        var tokenHandler = new JwtSecurityTokenHandler();
        ////        var securityToken = tokenHandler.CreateToken(tokenDesciptor);
        ////        var token = tokenHandler.WriteToken(securityToken);

        ////var cacheKey = token;

        //// IDistributedCache.GetString(cacheKey);

        ////this.distributedCache.SetString(cacheKey, token);

        ////      return this.Ok(new { token });
        ////    }
        ////    else
        ////    {
        ////        return this.BadRequest(new { message = "UserName And Password is InCorrect" });
        ////    }
        ////}

        ///// <summary>
        ///// Forgors the password.
        ///// </summary>
        ///// <param name="forgotPassword">The forgot password.</param>
        ///// <returns>result IActionResult</returns>
        ////[HttpPost]
        ////[Route("forgotpassword")]
        ////public async Task<IActionResult> ForgorPassword(ForgotPassword forgotPassword)
        ////     {
        ////       var user = await this.userManager.FindByEmailAsync(forgotPassword.Email);
        ////    if(user == null)
        ////    {
        ////        return this.BadRequest("User does Not Exist");
        ////   }
        ////    try
        ////    {
        ////        var code = await userManager.GeneratePasswordResetTokenAsync(user);
        ////        var callbackurl = "http://localhost:4200/user/resetpassword?UserId=" + user.Id + "&code="+code;
        //// var result= await ResetPassword(user.Id,callbackurl,user.Email);

        ////        BusinessLayer.EmailSender email = new BusinessLayer.EmailSender();
        ////        var result = email.SendEmail(user.Id, user.Email, "forgot Password", $"Please reset your password by clicking this link: < a href =\"" + callbackurl + "\">Link</a>");
        ////        return Ok(new { });
        ////    }
        ////    catch
        ////    {
        ////        throw;
        ////    }
        ////}

        ///// <summary>
        ///// Resets the password.
        ///// </summary>
        ///// <param name="resetPassword">The reset password.</param>
        ///// <returns>Result IActionResult</returns>
        ////[HttpPost]
        ////[Route("resetpassword")]
        ////public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        ////{
        ////    if(!ModelState.IsValid)
        ////    {
        ////       return this.BadRequest(ModelState);
        ////    }
        ////    var user = await userManager.FindByNameAsync(resetPassword.UserName);
        ////    if(user == null)
        ////    {
        ////        return BadRequest("User Not Valid");
        ////    }
        ////    var code = await userManager.GeneratePasswordResetTokenAsync(user);
        ////    var result = await userManager.ResetPasswordAsync(user, code, resetPassword.Password);
        ////    if (result.Succeeded)
        ////    {
        ////        return Ok();
        ////    }
        ////    return BadRequest("Internal Server Error");
        ////}

        /// <summary>
        /// The application user operation
        /// </summary>
        private readonly IApplicationUserOperations applicationUserOperation;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="applicationUserOperation">The application user operation.</param>
        public UserController(IApplicationUserOperations applicationUserOperation)
        {
            this.applicationUserOperation = applicationUserOperation;
        }

        /// <summary>
        /// Registers the specified registration control model.
        /// </summary>
        /// <param name="registrationControlModel">The registration control model.</param>
        /// <returns>user data</returns>
        [HttpPost]
       [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegistration registrationControlModel)
      {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await this.applicationUserOperation.PostApplicationUserAsync(registrationControlModel);
                    return this.Ok(new { result });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Logins the specified login control model.
        /// </summary>
        /// <param name="loginControlModel">The LoginControl.</param>
        /// <param name="fbStatus">The FbStatus.</param>
        /// <returns>login success result</returns>
        /// <exception cref="Exception">The Exception</exception>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<string> Login(LoginControl loginControlModel, string fbStatus)
        {
            try
            {
                var result = await this.applicationUserOperation.LoginAsync(loginControlModel, fbStatus);
                if (result == null)
                {
                    return "Invalide Email And Password";
                }

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Forgot the specified forgot password model.
        /// </summary>
        /// <param name="forgotPasswordModel">The forgot password model.</param>
        /// <returns>result data</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("forgotPassword")]
        public IActionResult Forgot(ForgotPassword forgotPasswordModel)
        {
            try
            {
                var result = this.applicationUserOperation.ForgotPasswordAsync(forgotPasswordModel);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// Resets the specified ResetPassword.
        /// </summary>
        /// <param name="resetPasswordmodel">The ResetPassword.</param>
        /// <returns>return status code</returns>
        [HttpPost]
        [Route("resetPassword")]
        [AllowAnonymous]
        public IActionResult Reset(ResetPassword resetPasswordmodel)
        {
            try
            {
                var result = this.applicationUserOperation.ResetPasswordAsync(resetPasswordmodel);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.BadRequest();
            }
        }

        /// <summary>
        /// ProfilePic the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>url data</returns>
        [HttpPost]
        [Route("profilepic/{userId}")]
        public IActionResult Profilepic(IFormFile file, string userId)
        {
            //// getting User data
            if (file == null) 
            {
                return this.NotFound("The file couldn't be found");
            }

            var result = this.applicationUserOperation.AddProfile(file, userId);
            return this.Ok(new { result });
        }

        /// <summary>
        /// Gets the GetProfilepic.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IActionResult Data</returns>
        [HttpGet]
        [Route("getprofilepic/{userId}")]
        public IActionResult GetProfilepic(string userId)
        {
            var result = this.applicationUserOperation.GetProfile(userId);
            
            return this.Ok(new { result });
        }
    }
}