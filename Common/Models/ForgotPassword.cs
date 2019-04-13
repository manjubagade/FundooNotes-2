using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesBackEnd.Models
{
    public class ForgotPassword
    {
        [Required]
        [Display(Name ="Email")]
        public string Email { get; set; }
    }
}
