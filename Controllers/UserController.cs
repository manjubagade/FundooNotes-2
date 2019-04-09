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
    using FundooNotesBackEnd.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

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
    }
}