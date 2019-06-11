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

    /// <summary>
    /// class For User Data Operations
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.IUserDataOperations" />
    public class UserDataOperations : IUserDataOperations
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationSettings applicationSettings;
        private readonly IDistributedCache distributedCache;
        private RegistrationControl registrationControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDataOperations"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="registrationControl">The registration control.</param>
        /// <param name="applicationSettings">The application settings.</param>
        /// <param name="distributedCache">The distributed cache.</param>
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
        public async Task<string> Login(LoginControl loginControlmodel,string FbStatus)
        {
            if (loginControlmodel.FbStatus == "false") { 
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
                    return "Invalide UserName And Password";
                }
            }
            else if (loginControlmodel.FbStatus == "true")
            {
                var user = await this.userManager.FindByEmailAsync(loginControlmodel.Email);
                if(user != null)
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
                    var applicationUser = new ApplicationUser()
                    {
                        
                        Email = loginControlmodel.Email,
                      
                    };
                    await this.userManager.CreateAsync(applicationUser, loginControlmodel.Password);
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
                
            }
            else
            {
                return "Bad Input";
            }
           // return "Invalid";
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
        public IList<ApplicationUser> GetProfilepic(string userId)
        {
            try
            {
                var list = new List<ApplicationUser>();
                var profile = from user in this.registrationControl.Application where (user.Id == userId) select user;
                return profile.ToArray();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
       
    }
}