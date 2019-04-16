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
    using Common.Models;
    using FundooNotesBackEnd.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Class UserController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// The application settings
        /// </summary>
        private readonly ApplicationSettings appSettings;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// The sign in manager
        /// </summary>
        private readonly SignInManager<ApplicationUser> signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="appSettings">The application settings.</param>
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appSettings = appSettings.Value;
        }

        /// <summary>
        /// Posts the user application.
        /// </summary>
        /// <param name="userRegistration">The user registration.</param>
        /// <returns>The object</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> PostUserApplication(UserRegistration userRegistration)
        {
            try
            {
                ////Assign Variables
                var applicationUser = new ApplicationUser()
                {
                    FirstName = userRegistration.FirstName,
                    Email = userRegistration.Email,
                    UserName = userRegistration.UserName
                };
                    ////Encrypted Password Assign Here
                    var result = await this.userManager.CreateAsync(applicationUser, userRegistration.Password);
                var token1 = this.userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                   var confirm =await this.userManager.ConfirmEmailAsync(token1);
                    return this.Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Logins the specified control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>The IActionResult</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginControl control)
        {
            ////Checking Name
            var user = await this.userManager.FindByNameAsync(control.UserName);
            ////Checking Name And Password
            if (user != null && await this.userManager.CheckPasswordAsync(user, control.Password))
            {
                var tokenDesciptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDesciptor);
                var token = tokenHandler.WriteToken(securityToken);
                return this.Ok(new { token });
            }
            else
            {
                return this.BadRequest(new { message = "UserName And Password is InCorrect" });
            }
        }

        /// <summary>
        /// Forgors the password.
        /// </summary>
        /// <param name="forgotPassword">The forgot password.</param>
        /// <returns>result IActionResult</returns>
        [HttpPost]
        [Route("forgotpassword")]
        public async Task<IActionResult> ForgorPassword(ForgotPassword forgotPassword)
             {
               var user = await this.userManager.FindByEmailAsync(forgotPassword.Email);
            if(user==null)
            {
                return BadRequest("User does Not Exist");
            }
            try
            {
                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = "http://localhost:4200/user/resetpassword?UserId=" + user.Id+"&code="+code;
               // var result= await ResetPassword(user.Id,callbackurl,user.Email);
              
                Services.EmailSender email = new Services.EmailSender();
                var result = email.SendEmail(user.Id, callbackurl, user.Email);
                return Ok(new { });
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns>Result IActionResult</returns>
        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if(user==null)
            {
                return BadRequest("Email Not Valid");
            }
            
            var result = await userManager.ResetPasswordAsync(user,resetPassword.Code,resetPassword.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Internal Server Error");
        }
    }
}