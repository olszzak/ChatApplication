using ChatApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginViewModel fi_loginInfo)
        {
            var result = _signInManager.PasswordSignInAsync(fi_loginInfo.username, fi_loginInfo.password, true, false).Result;
            if (result.Succeeded)
            {
                return Json(new { Message = "“Login success" });
            }
            else
            {
                return Json(new { Message = "Error Occurs" });
            }
        }
        public IActionResult Register([FromBody] LoginViewModel fi_loginInfo)
        {
            var user = new ApplicationUser() { UserName = fi_loginInfo.username, Email = fi_loginInfo.username };
            var result = _userManager.CreateAsync(user, fi_loginInfo.password).Result;
            if (result.Succeeded)
            {
                return Json(new { Message = "User Created" });
            }
            else
            {
                return Json(new { Message = "Error Occurs" });
            }
        }
    }
}
