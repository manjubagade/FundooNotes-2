using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    public class Notes : IdentityUser
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}
