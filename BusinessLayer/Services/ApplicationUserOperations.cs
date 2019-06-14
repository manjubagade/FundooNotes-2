// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUserOperations.cs" company="Bridgelabz">
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
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Options;
    using RepositoryLayer.Interface;

    /// <summary>
    /// Class For User Operations
    /// </summary>
    /// <seealso cref="BusinessLayer.Interfaces.IApplicationUserOperations" />
    public class ApplicationUserOperations : IApplicationUserOperations
    {
        /// <summary>
        /// The application repository
        /// </summary>
        private readonly IUserDataOperations applicationRepository;

        /// <summary>
        /// The email sender
        /// </summary>
        private readonly IEmailSender emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserOperations"/> class.
        /// </summary>
        /// <param name="applicationRepository">The application repository.</param>
        /// <param name="email">The email.</param>
        public ApplicationUserOperations(IUserDataOperations applicationRepository, IEmailSender email)
        {
            this.applicationRepository = applicationRepository;
            this.emailSender = email;
        }

        /// <summary>
        /// Posts the application user asynchronous.
        /// </summary>
        /// <param name="userRegistrationmodel">The UserRegistration.</param>
        /// <returns>
        /// data result
        /// </returns>
        public async Task<bool> PostApplicationUserAsync(UserRegistration userRegistrationmodel)
        {
            await this.applicationRepository.Register(userRegistrationmodel);
            return true;
        }

        /// <summary>
        /// Logins the asynchronous LoginControl.
        /// </summary>
        /// <param name="loginControlmodel">The LoginControl.</param>
        /// <returns>
        /// data result
        /// </returns>
        public async Task<string> LoginAsync(LoginControl loginControlmodel, string FbStatus)
        {
            return await applicationRepository.Login(loginControlmodel, FbStatus);
            
        }

        /// <summary>
        /// Forgot the password asynchronous.
        /// </summary>
        /// <param name="forgotPasswordmodel">The ForgotPassword.</param>
        /// <returns>
        /// data result
        /// </returns>
        public async Task<bool> ForgotPasswordAsync(ForgotPassword forgotPasswordmodel)
        {
            var result =  this.applicationRepository.FindByEmailAsync(forgotPasswordmodel);
            if (result != null)
            {
                var code = this.applicationRepository.GeneratePasswordResetTokenAsync(forgotPasswordmodel);
                var callbackUrl = "http://localhost:4200/user/resetpassword?code=" + code;
                
              await this.emailSender.SendEmailAsync(forgotPasswordmodel.Email, "Reset Password", $"Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">here</a>");
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Resets the ResetPassword asynchronous.
        /// </summary>
        /// <param name="resetPasswordmodel">The ResetPassword.</param>
        /// <returns>return result data</returns>
        public async Task<bool> ResetPasswordAsync(ResetPassword resetPasswordmodel)
        {
            var result = await this.applicationRepository.ResetPasswordAsync(resetPasswordmodel);
           
            if (result.Equals(true))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds the profile.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>string url data</returns>
        public string AddProfile(IFormFile file, string userId)
        {
            var result = this.applicationRepository.Profilepic(file, userId);
            return result;

        }

        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>user data</returns>
        public IList<ApplicationUser> GetProfile(string userId)
        {
            return this.applicationRepository.GetProfilepic(userId);
        }
    }
}