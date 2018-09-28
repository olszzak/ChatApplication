using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChatApplication.Models;
using ChatApplication.RabbitMq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Chat")]
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ChatController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public ActionResult Send(ReceivedMessage rm)
        {
            var myUserName = _userManager.GetUserName(HttpContext.User);
            
            try
            {
                var rabbit = new RabbitMqClient();
                rabbit.Send(rabbit.CreateConnection(), rm.RoutingKey, rm.Message);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Authorize]
        public ReceivedMessage Received()
        {
            try
            {
                var userName = _userManager.GetUserName(HttpContext.User);
                var rabbit = new RabbitMqClient();
                var received = rabbit.Receive(rabbit.CreateConnection(), userName);

                return received;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}