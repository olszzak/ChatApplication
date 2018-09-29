using System;
using System.Collections.Generic;
using System.Text;

namespace MessagesService.Models
{
    public class ReceivedMessage
    {
        public string Message { get; set; }
        public string RoutingKey { get; set; }
    }
}
