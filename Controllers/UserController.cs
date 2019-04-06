namespace FundooNotesBackEnd.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using FundooNotesBackEnd.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Object> PostUserApplication(UserRegistration userRegistration)
        {
            try
            {
                var ApplicationUser = new ApplicationUser()
                {
                    FirstName = userRegistration.FirstName,
                    Email = userRegistration.Email,
                    UserName = userRegistration.UserName
                };
                try { 
                    var Result = await _userManager.CreateAsync(ApplicationUser, userRegistration.Password);
                    return Ok(Result);
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginControl control)
        {
            var user =await _userManager.FindByNameAsync(control.UserName);
            if(user != null && await _userManager.CheckPasswordAsync(user,control.Password))
            {
                var tokenDesciptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.Minute(5),
                }
            }
        }
    }
}