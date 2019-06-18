// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationUserOperations.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace BusinessLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FundooApi;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// interface IApplicationUserOperations
    /// </summary>
    public interface IApplicationUserOperations
    {
        /// <summary>
        /// Posts the application user asynchronous.
        /// </summary>
        /// <param name="userRegistrationmodel">The UserRegistration.</param>
        /// <returns>data result</returns>
        Task<bool> PostApplicationUserAsync(UserRegistration userRegistrationmodel);

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="loginControlmodel">The LoginControl.</param>
        /// <param name="FbStatus">The FbStatus.</param>
        /// <returns>data result</returns>
        Task<string> LoginAsync(LoginControl loginControlmodel, string FbStatus);

        /// <summary>
        /// Forgot the password asynchronous.
        /// </summary>
        /// <param name="forgotPasswordmodel">The ForgotPassword.</param>
        /// <returns>data result</returns>
        Task<bool> ForgotPasswordAsync(ForgotPassword forgotPasswordmodel);

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="resetPasswordmodel">The ResetPassword.</param>
        /// <returns>data result</returns>
        Task<bool> ResetPasswordAsync(ResetPassword resetPasswordmodel);

        /// <summary>
        /// Adds the profile.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>result string</returns>
        string AddProfile(IFormFile file, string userId);

        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>user data</returns>
        IList<ApplicationUser> GetProfile(string userId);
    }
}