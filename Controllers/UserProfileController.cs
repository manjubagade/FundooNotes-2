// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="UserProfileController.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
namespace FundooNotesBackEnd.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooNotesBackEnd.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// class UserProfileController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <returns>The object</returns>
        [HttpGet]
        [Authorize]
        public async Task<object> GetUserProfile()
        {
            ////Get As Per Valide UserId
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await this.userManager.FindByIdAsync(userId);
            return new
            {
                user.UserName,
                user.Email,
                user.FirstName
            };
        }
    }
}