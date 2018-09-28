using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
    public class ReceivedMessage
    {
        public string Message { get; set; }
        public string RoutingKey { get; set; }
    }
}
