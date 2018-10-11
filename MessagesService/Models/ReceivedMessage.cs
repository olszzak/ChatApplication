// TODO BP: przed pushem powinieneś robić optimize usings (czy coś w tym stylu, nie pamiętam jak to się nazywa w VS), co posortuje i usunie nieużywane referencje
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
