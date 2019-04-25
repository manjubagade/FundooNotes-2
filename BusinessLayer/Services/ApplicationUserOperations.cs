using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FundooApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using RepositoryLayer.Interface;

namespace BusinessLayer.Interfaces
{
    public class ApplicationUserOperations:IApplicationUserOperations
    {
        private readonly IUserDataOperations applicationRepository;

        private readonly IEmailSender emailSender;


        public ApplicationUserOperations(IUserDataOperations applicationRepository,IEmailSender emailSender)
        {
            this.applicationRepository = applicationRepository;
            this.emailSender = emailSender;
        }

        public async Task<bool> PostApplicationUserAsync(UserRegistration userRegistrationmodel)
        {
          await this.applicationRepository.Register(userRegistrationmodel);
            return true;
        }

        public async Task<string> LoginAsync(LoginControl loginControlmodel)
        {
            string jsonstring = await applicationRepository.Login(loginControlmodel);
            return jsonstring;
        }

        public bool ForgotPasswordAsync(ForgotPassword forgotPasswordmodel)
        {
            var result = this.applicationRepository.FindByEmailAsync(forgotPasswordmodel);
            if (result != null)
            {
                var code = this.applicationRepository.GeneratePasswordResetTokenAsync(forgotPasswordmodel);
                var callbackUrl = "http://localhost:4200/resetpassword?code=" + code;
                this.emailSender.SendEmailAsync(forgotPasswordmodel.Email, "Reset Password", $"Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">here</a>");
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
        public bool ResetPasswordAsync(ResetPassword resetPasswordmodel)
        {
            this.applicationRepository.ResetPasswordAsync(resetPasswordmodel);
            return true;
        }
    }
}