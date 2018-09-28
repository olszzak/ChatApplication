using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ChatApplication.Models
{
    public class IndexViewModel
    {
        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
        public string Message { get; set; }
        public string RoutingKey { get; set; }
    }  
}
