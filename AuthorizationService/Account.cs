using AuthorizationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationService
{
    public class Account : IAccount
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Account(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Microsoft.AspNetCore.Identity.SignInResult SignIn([FromForm] LoginViewModel fi_loginInfo)
        {
            return _signInManager.PasswordSignInAsync(fi_loginInfo.username, fi_loginInfo.password, true, false).Result;
        }

        public ApplicationUser Register([FromBody] LoginViewModel fi_loginInfo)
        {
            return new ApplicationUser() { UserName = fi_loginInfo.username, Email = fi_loginInfo.username };
        }

        public IdentityResult Create(ApplicationUser user, [FromBody] LoginViewModel fi_loginInfo)
        {
            return _userManager.CreateAsync(user, fi_loginInfo.password).Result;
        }

        public string GetUserName(HttpContext cont)
        {
           return _userManager.GetUserName(cont.User);
        }
        public IEnumerable<ApplicationUser> GetOtherUsers(HttpContext cont, string myUserName)
        {
            return _userManager.Users.Where(u => u.UserName != myUserName).ToList();
        }
    }
}
