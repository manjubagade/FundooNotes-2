namespace RepositoryLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FundooApi;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using RepositoryLayer.Interface;

    public class UserDataOperations : IUserDataOperations
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationSettings appSettings;
        public UserDataOperations(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appSettings = appSettings.Value;
        }
        public async Task<IActionResult> RegisterData(UserRegistration userRegistration)
        {
            ////Assign Variables
            var applicationUser = new ApplicationUser()
            {
                FullName = userRegistration.FullName,
                Email = userRegistration.Email,
                UserName = userRegistration.UserName
            };
            ////Encrypted Password Assign Here
            var result = await this.userManager.CreateAsync(applicationUser, userRegistration.Password);
            return null;
        }

        Task IUserDataOperations.RegisterData(UserRegistration userRegistration)
        {
            throw new NotImplementedException();
        }
    }
}