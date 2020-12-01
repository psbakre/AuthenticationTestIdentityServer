using AuthTestIdentityServer.Models;
using AuthTestIdentityServer.Models.Dto;
using AuthTestIdentityServer.ViewModels.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTestIdentityServer.Controllers
{
    [Route("[controller]/[action]")]
    //[ApiController]
    public class AuthenticateController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration _configuration;


        [TempData]
        public string ErrorMessage { get; set; }
        public AuthenticateController(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            LoginViewModel loginViewModel = new ();
            return View(loginViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromForm]Login login,[FromQuery]string ReturnUrl)
        {
            LoginViewModel loginViewModel = new ();
            if (ModelState.IsValid) { 
                var user=await userManager.FindByNameAsync(login.Username);
                if (user!=null)
                {
                    var result = await signInManager.PasswordSignInAsync(login.Username, login.Password, login.Persist, false);
                    if (result.Succeeded)
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Incorrect Password");
                    }
                    
                    
                } else
                {
                    ModelState.AddModelError("Username", "User not found");
                }
                
                loginViewModel.login = login;
                return View(loginViewModel);
            }


            //ModelState.AddModelError(string.Empty, "Invalid State");
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register (Register register)
        {
            string returnUrl = "/weatherforecast";
            var user = new User
            {
                UserName = register.Username,
                Email= register.Email

            };
            var result =await  userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);
            }
            return StatusCode(StatusCodes.Status406NotAcceptable, "User exists");
        }
    }
}
