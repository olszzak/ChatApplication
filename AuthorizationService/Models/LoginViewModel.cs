using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationService.Models
{
    public class LoginViewModel
    {
        // Kuba: zmienne tu w modelu z wielkiej litery
        public string username { get; set; }
        public string password { get; set; }
    }
}
