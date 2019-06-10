// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUserOperations.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
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

namespace BusinessLayer.Interfaces
{
    /// <summary>
    /// Class For User Operations
    /// </summary>
    /// <seealso cref="BusinessLayer.Interfaces.IApplicationUserOperations" />
    public class ApplicationUserOperations:IApplicationUserOperations
    {
        private readonly IUserDataOperations applicationRepository;

        private readonly IEmailSender emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserOperations"/> class.
        /// </summary>
        /// <param name="applicationRepository">The application repository.</param>
        /// <param name="email">The email.</param>
        public ApplicationUserOperations(IUserDataOperations applicationRepository,IEmailSender email)
        {
            this.applicationRepository = applicationRepository;
            this.emailSender = email;
        }

        /// <summary>
        /// Posts the application user asynchronous.
        /// </summary>
        /// <param name="userRegistrationmodel">The user registrationmodel.</param>
        /// <returns>
        /// bool result
        /// </returns>
        public async Task<bool> PostApplicationUserAsync(UserRegistration userRegistrationmodel)
        {
            await this.applicationRepository.Register(userRegistrationmodel);
            return true;
        }

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="loginControlmodel">The login controlmodel.</param>
        /// <returns>
        /// bool result
        /// </returns>
        public async Task<string> LoginAsync(LoginControl loginControlmodel, string FbStatus)
        {
            return await applicationRepository.Login(loginControlmodel, FbStatus);
            
        }

        /// <summary>
        /// Forgots the password asynchronous.
        /// </summary>
        /// <param name="forgotPasswordmodel">The forgot passwordmodel.</param>
        /// <returns>
        /// bool result
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
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>return boolean</returns>
        public async Task<bool> ResetPasswordAsync(ResetPassword resetPasswordmodel)
        {
            var result= await this.applicationRepository.ResetPasswordAsync(resetPasswordmodel);
           
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
        /// <returns></returns>
        public string addProfile(IFormFile file, string userId)
        {
            var result= this.applicationRepository.Profilepic(file, userId);
            return result;

        }

        /// <summary>
        /// Gets the profile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IList<ApplicationUser> getProfile(string userId)
        {
            return this.applicationRepository.GetProfilepic(userId);
        }
    }
}