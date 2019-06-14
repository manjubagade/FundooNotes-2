// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserDataOperations.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FundooApi;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// IUserDataOperations for Handling User Operations
    /// </summary>
    public interface IUserDataOperations
    {
        /// <summary>
        /// Registers the specified user registration.
        /// </summary>
        /// <param name="userRegistration">The user registration.</param>
        /// <returns>Task return any data</returns>
        Task Register(UserRegistration userRegistration);

        /// <summary>
        /// Logins the specified LoginControl.
        /// </summary>
        /// <param name="loginControlmodel">The LoginControl.</param>
        /// <returns>string data</returns>
        Task<string> Login(LoginControl loginControlmodel, string FbStatus);

        /// <summary>
        /// Finds the by email asynchronous.
        /// </summary>
        /// <param name="forgotPasswordmodel">The forgotPasswordmodel.</param>
        /// <returns>token data</returns>
        Task FindByEmailAsync(ForgotPassword forgotPasswordmodel);

        /// <summary>
        /// Generates the password reset token asynchronous.
        /// </summary>
        /// <param name="forgotPasswordmodel">The ForgotPassword.</param>
        /// <returns>string token</returns>
        Task<string> GeneratePasswordResetTokenAsync(ForgotPassword forgotPasswordmodel);

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="resetPasswordmodel">The ResetPassword.</param>
        /// <returns>result data</returns>
        Task<object> ResetPasswordAsync(ResetPassword resetPasswordmodel);

        /// <summary>
        /// Profilepic the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>string url</returns>
        string Profilepic(IFormFile file, string userId);

        /// <summary>
        /// Gets the pic.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>user data</returns>
        IList<ApplicationUser> GetProfilepic(string userId);
    }
}