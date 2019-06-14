// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginControl.cs" company="Bridgelabz">
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

    /// <summary>
    /// LoginControl Class
    /// </summary>
    public class LoginControl
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the FbStatus.
        /// </summary>
        /// <value>
        /// The FbStatus.
        /// </value>
        public string FbStatus { get; set; }
    }
}
