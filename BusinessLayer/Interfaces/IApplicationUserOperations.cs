// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationUserOperations.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace BusinessLayer.Interfaces
{
    using FundooApi;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IApplicationUserOperations
    {
        /// <summary>
        /// Posts the application user asynchronous.
        /// </summary>
        /// <param name="userRegistrationmodel">The user registrationmodel.</param>
        /// <returns>bool result</returns>
        Task<bool> PostApplicationUserAsync(UserRegistration userRegistrationmodel);

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="loginControlmodel">The login controlmodel.</param>
        /// <returns>bool result</returns>
        Task<string> LoginAsync(LoginControl loginControlmodel);

        /// <summary>
        /// Forgots the password asynchronous.
        /// </summary>
        /// <param name="forgotPasswordmodel">The forgot passwordmodel.</param>
        /// <returns>bool result</returns>
        Task<bool> ForgotPasswordAsync(ForgotPassword forgotPasswordmodel);

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="resetPasswordmodel">The reset passwordmodel.</param>
        /// <returns>bool result</returns>
        Task<bool> ResetPasswordAsync(ResetPassword resetPasswordmodel);

        /// <summary>
        /// Adds the profile.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        string addProfile(IFormFile file, string userId);

        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<ApplicationUser> getProfile(string userId);
    }
}