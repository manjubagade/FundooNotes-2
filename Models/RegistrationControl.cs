using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesBackEnd.Models
{
    public class RegistrationControl : IdentityDbContext
    {
        public RegistrationControl(DbContextOptions options):base(options)
        {
        }
      //  public DbSet<UserRegistration> applicationUsers { get; set; }
        public DbSet<ApplicationUser> application { get; set; }
    }
}