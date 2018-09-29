using AuthorizationService;
using AuthorizationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccount _account;
        public AccountController(IAccount account)
        {
            _account = account;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginViewModel fi_loginInfo)
        {
            var result = _account.SignIn(fi_loginInfo);
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
            var user = _account.Register(fi_loginInfo);
            var result = _account.Create(user, fi_loginInfo) ;
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
