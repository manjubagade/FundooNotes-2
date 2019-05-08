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
         Task<bool> PostApplicationUserAsync(UserRegistration userRegistrationmodel);

         Task<string> LoginAsync(LoginControl loginControlmodel);
     
         bool ForgotPasswordAsync(ForgotPassword forgotPasswordmodel);
      
         bool ResetPasswordAsync(ResetPassword resetPasswordmodel);

        
    }
}