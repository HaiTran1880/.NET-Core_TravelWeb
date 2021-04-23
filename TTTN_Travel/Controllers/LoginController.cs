using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTTN_Travel.Models;

namespace TTTN_Travel.Controllers
{
    public class LoginController : Controller
    {
        private readonly TourReContext tourReContext = new TourReContext();

        public LoginController(TourReContext context)
        {
            this.tourReContext = context;
        }
        public IActionResult Index()
        {
            ViewBag.Err = HttpContext.Session.GetString("Err");
            return View();
        }

        public IActionResult LoginPost()
        {
            string name = Request.Form["Username"];
            string pass = Request.Form["Password"];

            //ma hoa password
            //_password = Security.Encrypt(_password.ToString());
            var acc= this.tourReContext.Admin.Where(x => x.Username.Equals(name) && x.Passwork.Equals(pass)).FirstOrDefault();
            ViewBag.Acc = this.tourReContext.Admin.Where(x => x.Username.Equals(name) && x.Passwork.Equals(pass)).FirstOrDefault();
            if (ViewBag.Acc != null)
            {
                //đăng nhập thành công
                //sét biến session  để kiểm tra các trang trong admin
                HttpContext.Session.SetString("name", acc.Username);
                HttpContext.Session.SetString("pass", acc.Passwork);
                HttpContext.Session.SetString("image", acc.Image);
                HttpContext.Session.SetString("Account", name);
                HttpContext.Session.Remove("Err");
                return Redirect("/Admin/Home");
            }
            else
            {
                HttpContext.Session.SetString("Err", "Thông tin không đúng vui lòng kiểm  tra lại!");
                
                return RedirectToAction("Index", "Login");
            }    
        }
    }
}
