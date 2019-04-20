// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="ForgotPassword.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
namespace FundooApi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class ForgotPassword
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
