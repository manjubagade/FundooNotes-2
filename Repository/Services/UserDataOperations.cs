﻿// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDataOperations.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FundooApi;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Interface;

    public class UserDataOperations : IUserDataOperations
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationSettings applicationSettings;
        private readonly IDistributedCache distributedCache;
        private RegistrationControl registrationControl;
        public UserDataOperations(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, RegistrationControl registrationControl, IOptions<ApplicationSettings> applicationSettings, IDistributedCache distributedCache)
        {
            this.registrationControl = registrationControl;
            this.userManager = userManager;
             this.signInManager = signInManager;
             this.applicationSettings = applicationSettings.Value;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Registers the specified user registration.
        /// </summary>
        /// <param name="userRegistration">The user registration.</param>
        /// <returns></returns>
        public Task Register(UserRegistration userRegistration)
        {
            ////Assign Variables
            var applicationUser = new ApplicationUser()
            {
                FullName = userRegistration.FullName,
                Email = userRegistration.Email,
                UserName = userRegistration.UserName
            };
            ////Encrypted Password Assign Here
            var result =  this.userManager.CreateAsync(applicationUser, userRegistration.Password);
            return result;
        }

        /// <summary>
        /// Logins the specified login controlmodel.
        /// </summary>
        /// <param name="loginControlmodel">The login controlmodel.</param>
        /// <returns></returns>
        public async Task<string> Login(LoginControl loginControlmodel)
        {
           
                var user = await this.userManager.FindByEmailAsync(loginControlmodel.Email);
                if (user != null && await this.userManager.CheckPasswordAsync(user, loginControlmodel.Password))
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[] { new Claim("UserID", user.Id.ToString()) }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.applicationSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    var cacheKey = token;
                    this.distributedCache.GetString(cacheKey);
                    this.distributedCache.SetString(cacheKey, token);
                
                    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(token);
                    return jsonString;
                }
                else
                {
                    return null;
                }
            }

        /// <summary>
        /// Finds the by email asynchronous.
        /// </summary>
        /// <param name="forgotPasswordmodel">The forgot passwordmodel.</param>
        /// <returns></returns>
        public Task FindByEmailAsync(ForgotPassword forgotPasswordmodel)
        {
            var result = this.userManager.FindByEmailAsync(forgotPasswordmodel.Email);
            return result;
        }

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="resetPasswordmodel">The reset passwordmodel.</param>
        /// <returns>Result for reset</returns>
        public async Task<object> ResetPasswordAsync(ResetPassword resetPasswordmodel)
        {
            var userName = await this.userManager.FindByEmailAsync(resetPasswordmodel.UserName);
            var token = await this.userManager.GeneratePasswordResetTokenAsync(userName);
            var result = await this.userManager.ResetPasswordAsync(userName, token, resetPasswordmodel.Password);
            return result;
        }

        /// <summary>
        /// Generates the password reset token asynchronous.
        /// </summary>
        /// <param name="forgotPasswordmodel">The forgot passwordmodel.</param>
        /// <returns></returns>
        public async Task<string> GeneratePasswordResetTokenAsync(ForgotPassword forgotPasswordmodel)
        {
            var result = await this.userManager.FindByEmailAsync(forgotPasswordmodel.Email);
            var user = await this.userManager.GenerateEmailConfirmationTokenAsync(result);
            return user;
        }

        /// <summary>
        /// Profilepics the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public string Profilepic(IFormFile file, string userId)
        {
            var stream = file.OpenReadStream();
            var name = file.FileName;
            Account account = new Account("dc1kbrrhk", "383789512449669", "fqD5389o6BAzQiFaUk56zQzsYyM");
            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, stream)
            };
            var uploadResult = cloudinary.Upload(uploadParams);
           
            var data = this.registrationControl.Application.Where(t => t.Id==userId).FirstOrDefault();
            data.Image = uploadResult.Uri.ToString();
            int result = 0;
            try
            {
                result = this.registrationControl.SaveChanges();
                 return data.Image;
              
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Gets the profilepic.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public string GetProfilepic(string userId)
        {
            try
            {
             ApplicationUser profile = this.registrationControl.Application.Where<ApplicationUser>(c => c.Id == userId).SingleOrDefault();
                return profile.Image.ToString();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}