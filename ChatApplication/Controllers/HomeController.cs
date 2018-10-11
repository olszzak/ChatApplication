using System.Linq;
using AuthorizationService;
using AuthorizationService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChatApplication.Controllers
{
    public class HomeController : Controller
    {
        // TODO BP: nigdzie tego mappera nie wykorzystujesz
        private readonly IMapper _mapper;
        private readonly IAccount _account;

        public HomeController(IAccount account, IMapper mapper)
        {
            _account = account;
            _mapper = mapper;
        }

        [Authorize]
        public ActionResult Index()
        {
            var myUserName = _account.GetUserName(HttpContext);

            var users = _account.GetOtherUsers(HttpContext, myUserName);
            var viewModel = new IndexViewModel();

            foreach (var item in users)
            {
                viewModel.Users.Add(new SelectListItem
                {
                    Value = item.UserName,
                    Text = item.UserName
                });
            }

            return View("Index", viewModel);
        }
    }
}
