// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserDataOperations.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

using FundooApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserDataOperations
    {
        Task Register(UserRegistration userRegistration);

        Task<string> Login(LoginControl loginControlmodel);

        Task FindByEmailAsync(ForgotPassword forgotPasswordmodel);

        Task<string> GeneratePasswordResetTokenAsync(ForgotPassword forgotPasswordmodel);

        Task<object> ResetPasswordAsync(ResetPassword resetPasswordmodel);
    }
}