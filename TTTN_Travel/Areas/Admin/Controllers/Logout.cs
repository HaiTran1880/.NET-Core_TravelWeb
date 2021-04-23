using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTTN_Travel.Areas.Admin.Controllers
{
    public class Logout : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            HttpContext.Session.Remove("Account");
            HttpContext.Session.Remove("name");
            HttpContext.Session.Remove("pass");
            HttpContext.Session.Remove("image");
            //di chuyen den url /Login
            return Redirect("/Login/Index");
        }
    }
}
