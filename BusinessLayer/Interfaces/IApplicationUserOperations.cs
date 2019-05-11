using FundooApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
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
        bool ForgotPasswordAsync(ForgotPassword forgotPasswordmodel);

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="resetPasswordmodel">The reset passwordmodel.</param>
        /// <returns>bool result</returns>
        bool ResetPasswordAsync(ResetPassword resetPasswordmodel);

        
    }
}