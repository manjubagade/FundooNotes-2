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
    using FundooApi;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json.Linq;
    using Experimental.System.Messaging;

    /// <summary>
    /// Class UserController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ///// <summary>
        ///// The application settings
        ///// </summary>
        //private readonly ApplicationSettings appSettings;

        ///// <summary>
        ///// The user manager
        ///// </summary>
        //private readonly UserManager<ApplicationUser> userManager;

        ///// <summary>
        ///// The sign in manager
        ///// </summary>
        //private readonly SignInManager<ApplicationUser> signInManager;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="UserController"/> class.
        ///// </summary>
        ///// <param name="userManager">The user manager.</param>
        ///// <param name="signInManager">The sign in manager.</param>
        ///// <param name="appSettings">The application settings.</param>
        //public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
        //{
        //    this.userManager = userManager;
        //    this.signInManager = signInManager;
        //    this.appSettings = appSettings.Value;
        //}

        ///// <summary>
        ///// Posts the user application.
        ///// </summary>
        ///// <param name="userRegistration">The user registration.</param>
        ///// <returns>The object</returns>
        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> PostUserApplication(UserRegistration userRegistration)
        //{
        //    try
        //    {
        //        ////Assign Variables
        //        var applicationUser = new ApplicationUser()
        //        {
        //            FullName = userRegistration.FullName,
        //            Email = userRegistration.Email,
        //            UserName = userRegistration.UserName
        //        };
        //            ////Encrypted Password Assign Here
        //            var result = await this.userManager.CreateAsync(applicationUser, userRegistration.Password);
        //        if (result.Succeeded)
        //        {
        //            var user = await this.userManager.FindByNameAsync(userRegistration.UserName);
        //            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
        //            string callBackUrl = Url.Link("ConfirmEmail", new { userName = user, code = code });
        //            BusinessLayer.EmailSender email = new BusinessLayer.EmailSender();
        //            await email.SendEmail(user.Id, userRegistration.Email, "Confirm your account", $"Please confirm your account by clicking this link: < a href =\"" + callBackUrl + "\" ></a>");

        //        }
        //        return this.Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //[Route("ConfirmEmail", Name = "ConfirmEmail")]
        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IActionResult> ConfirmEmail(string userName, string code)
        //{

        //    var user = await this.userManager.FindByNameAsync(userName);
        //    if (user == null || code == null)
        //    {
        //        return this.NotFound();
        //    }

        //    var result = await this.userManager.ConfirmEmailAsync(user, code);
        //    return this.Ok(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        ///// <summary>
        ///// Logins the specified control.
        ///// </summary>
        ///// <param name="control">The control.</param>
        ///// <returns>The IActionResult</returns>
        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login(LoginControl control)
        //{
        //    ////Checking Name
        //    var user = await this.userManager.FindByNameAsync(control.UserName);
        //    ////Checking Name And Password
        //    if (user != null && await this.userManager.CheckPasswordAsync(user, control.Password))
        //    {
        //        var tokenDesciptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new Claim[]
        //            {
        //                new Claim("UserId", user.Id.ToString())
        //            }),
        //            Expires = DateTime.UtcNow.AddDays(1),
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
        //        };
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var securityToken = tokenHandler.CreateToken(tokenDesciptor);
        //        var token = tokenHandler.WriteToken(securityToken);

                 //var cacheKey = token;

                 // IDistributedCache.GetString(cacheKey);

                 //this.distributedCache.SetString(cacheKey, token);

        //        return this.Ok(new { token });
        //    }
        //    else
        //    {
        //        return this.BadRequest(new { message = "UserName And Password is InCorrect" });
        //    }
        //}

        ///// <summary>
        ///// Forgors the password.
        ///// </summary>
        ///// <param name="forgotPassword">The forgot password.</param>
        ///// <returns>result IActionResult</returns>
        //[HttpPost]
        //[Route("forgotpassword")]
        //public async Task<IActionResult> ForgorPassword(ForgotPassword forgotPassword)
        //     {
        //       var user = await this.userManager.FindByEmailAsync(forgotPassword.Email);
        //    if(user == null)
        //    {
        //        return this.BadRequest("User does Not Exist");
        //    }
        //    try
        //    {
        //        var code = await userManager.GeneratePasswordResetTokenAsync(user);
        //        var callbackurl = "http://localhost:4200/user/resetpassword?UserId=" + user.Id + "&code="+code;
        //       // var result= await ResetPassword(user.Id,callbackurl,user.Email);

        //        BusinessLayer.EmailSender email = new BusinessLayer.EmailSender();
        //        var result = email.SendEmail(user.Id, user.Email, "forgot Password", $"Please reset your password by clicking this link: < a href =\"" + callbackurl + "\">Link</a>");
        //        return Ok(new { });
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Resets the password.
        ///// </summary>
        ///// <param name="resetPassword">The reset password.</param>
        ///// <returns>Result IActionResult</returns>
        //[HttpPost]
        //[Route("resetpassword")]
        //public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return this.BadRequest(ModelState);
        //    }
        //    var user = await userManager.FindByNameAsync(resetPassword.UserName);
        //    if(user == null)
        //    {
        //        return BadRequest("User Not Valid");
        //    }
        //    var code = await userManager.GeneratePasswordResetTokenAsync(user);
        //    var result = await userManager.ResetPasswordAsync(user, code, resetPassword.Password);
        //    if (result.Succeeded)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest("Internal Server Error");
        //}


        private readonly IApplicationUserOperations applicationUserOperation;


        public UserController(IApplicationUserOperations applicationUserOperation)
        {
            this.applicationUserOperation = applicationUserOperation;
        }

        
        [HttpPost]
       [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegistration registrationControlModel)
      {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await applicationUserOperation.PostApplicationUserAsync(registrationControlModel);
                    return this.Ok(new { result });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return this.BadRequest();
        }
        
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<string> Login(LoginControl loginControlModel,string FbStatus)
        {
            try
            {
                var result = await applicationUserOperation.LoginAsync(loginControlModel, FbStatus);
                if(result==null){
                    return "Invalide Email And Password";
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
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
        /// Resets the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return status code</returns>
        [HttpPost]
        [Route("resetPassword")]
        
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
        /// Profilepics the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("profilepic/{userId}")]
        public IActionResult Profilepic(IFormFile file, string userId)
        {
            if (file == null) //business Layer
            {
                return this.NotFound("The file couldn't be found");
            }

            var result = this.applicationUserOperation.addProfile(file, userId);
            return this.Ok(new { result });
        }

        [HttpGet]
        [Route("getprofilepic/{userId}")]
        public IActionResult GetProfilepic(string userId)
        {
            var result = this.applicationUserOperation.getProfile(userId);
            
            return this.Ok(new { result });
        }

    }
}