// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserDataOperations.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Interface
{
    using FundooApi;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// IUserDataOperations for Handling User Operations
    /// </summary>
    public interface IUserDataOperations
    {
        /// <summary>
        /// Registers the specified user registration.
        /// </summary>
        /// <param name="userRegistration">The user registration.</param>
        /// <returns></returns>
        Task Register(UserRegistration userRegistration);

        /// <summary>
        /// Logins the specified login controlmodel.
        /// </summary>
        /// <param name="loginControlmodel">The login controlmodel.</param>
        /// <returns></returns>
        Task<string> Login(LoginControl loginControlmodel);

        /// <summary>
        /// Finds the by email asynchronous.
        /// </summary>
        /// <param name="forgotPasswordmodel">The forgot passwordmodel.</param>
        /// <returns></returns>
        Task FindByEmailAsync(ForgotPassword forgotPasswordmodel);

        /// <summary>
        /// Generates the password reset token asynchronous.
        /// </summary>
        /// <param name="forgotPasswordmodel">The forgot passwordmodel.</param>
        /// <returns></returns>
        Task<string> GeneratePasswordResetTokenAsync(ForgotPassword forgotPasswordmodel);

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="resetPasswordmodel">The reset passwordmodel.</param>
        /// <returns></returns>
        Task<object> ResetPasswordAsync(ResetPassword resetPasswordmodel);

        /// <summary>
        /// Profilepics the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        string Profilepic(IFormFile file, string userId);

        /// <summary>
        /// Gets the profilepic.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        string GetProfilepic(string userId);
    }
}