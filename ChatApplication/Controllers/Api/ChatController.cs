using AuthorizationService;
using MessagesService;
using MessagesService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChatApplication.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Chat")]
    public class ChatController : Controller
    {
        private readonly IRabbitMqClient _rabbitMqClient;
        private readonly IAccount _account;
        public ChatController(IAccount account, IRabbitMqClient rabbitMqClient)
        {
            _rabbitMqClient = rabbitMqClient;
            _account = account;
            // TODO BP: formatowanie
    }

        // TODO BP: brakuje głębszej walidacji obiektu który przychodzi z frontu. co jak na danym serwerze rabbitmq chodzą inne aplikacje poza tym czatem? ktoś z zewnątrz mógłby do nich wysyłać wiadomości
        //          w dodatku brak tu autoryzacji.
        [HttpPost]
        public ActionResult Send(ReceivedMessage rm)
        {
            // TODO BP: nieużywany var
            var myUserName = _account.GetUserName(HttpContext);

            try
            {
              //  var rabbit = new RabbitMqClient();
                _rabbitMqClient.Send(_rabbitMqClient.CreateConnection(), rm.RoutingKey, rm.Message);

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
                var userName = _account.GetUserName(HttpContext);
               // var rabbit = new RabbitMqClient();
                var received = _rabbitMqClient.Receive(_rabbitMqClient.CreateConnection(), userName);

                return received;
            }
            catch (Exception)
            {
                // TODO BP: to bardziej tu pasuje.
                // return NoContent();
                return null;
            }
        }
    }
}
