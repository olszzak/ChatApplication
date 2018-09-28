using AutoMapper;
using ChatApplication.Models;
using ChatApplication.RabbitMq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [Authorize]
        public ActionResult Index()
        {
            var myUserName = _userManager.GetUserName(HttpContext.User);

            var users = _userManager.Users.Where(u=>u.UserName != myUserName).ToList();
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
