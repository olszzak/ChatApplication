using AuthorizationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AuthorizationService
{
    public interface IAccount
    {
        // Kuba: atrybuty [FromForm] i [FromBody] muszą być tylko w kontrolerach, z tego interfejsu i implementacji możesz je wywalić 
        Microsoft.AspNetCore.Identity.SignInResult SignIn([FromForm] LoginViewModel fi_loginInfo);
        ApplicationUser Register([FromBody] LoginViewModel fi_loginInfo);
        IdentityResult Create(ApplicationUser user, [FromBody] LoginViewModel fi_loginInfo);
        string GetUserName(HttpContext cont);
        IEnumerable<ApplicationUser> GetOtherUsers(HttpContext cont, string myUserName);
    }
}