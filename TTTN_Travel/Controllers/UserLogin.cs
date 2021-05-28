using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TTTN_Travel.Models;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
namespace TTTN_Travel.Controllers
{
    public class UserLogin : Controller
    {
        private readonly TourReContext tourReContext = new TourReContext();
        private readonly IEmailService _EmailService;
        public UserLogin(TourReContext context, IEmailService emailService)
        {
            this.tourReContext = context;
            _EmailService = emailService;
        }
        public IActionResult Index()
        {
            ViewBag.Er = HttpContext.Session.GetString("Er");
            return View();
        }
        public IActionResult Login()
        {

            string name = Request.Form["Username"];
            string pass = Request.Form["Password"];

            //ma hoa password
            //_password = Security.Encrypt(_password.ToString());
            var acc = this.tourReContext.User.Where(x => x.Username.Equals(name) && x.Pass.Equals(pass)).FirstOrDefault();
            ViewBag.Acc = this.tourReContext.User.Where(x => x.Username.Equals(name) && x.Pass.Equals(pass)).FirstOrDefault();
            if (ViewBag.Acc != null)
            {
                //đăng nhập thành công
                //sét biến session  để kiểm tra
                HttpContext.Session.SetString("name", acc.Username);
                HttpContext.Session.SetString("ten", acc.Name);
                HttpContext.Session.SetString("pass", acc.Pass);
                //HttpContext.Session.SetString("image", acc.Image);
                HttpContext.Session.SetString("id", acc.Id.ToString());
                HttpContext.Session.SetString("phone", acc.Phone);
                HttpContext.Session.SetString("email", acc.Email);
                HttpContext.Session.SetString("adress", acc.Adress);
                HttpContext.Session.SetString("Check", "Logined");
                HttpContext.Session.Remove("Er");
                return Redirect("/Home/Index");
            }
            else
            {
                HttpContext.Session.SetString("Er", "Thông tin không đúng vui lòng kiểm  tra lại!");

                return RedirectToAction("Index", "UserLogin");
            }
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("name");
            HttpContext.Session.Remove("pass");
            HttpContext.Session.Remove("image");
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("phone");
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("adress");
            HttpContext.Session.Remove("Check");
            HttpContext.Session.Remove("ten");
            //di chuyen den url /
            return Redirect("/Home/Index");
        }
        public IActionResult Regis()
        {
            
            return View();
        }
        public IActionResult ConfirmRegis()
        {
            try
            {
                User us = new User();
                us.Name = Request.Form["first_name"];
                us.Username = Request.Form["user_name"];
                us.Pass = Request.Form["user_password"];
                us.Adress = Request.Form["adress"];
                us.Email = Request.Form["email"];
                us.Phone = Request.Form["contact_no"];
                us.Cmt = Request.Form["cmt"];
                this.tourReContext.User.Add(us);
                this.tourReContext.SaveChanges();

            }
            catch(Exception e)
            {
                Console.Write(e);
            }
            return RedirectToAction("Index","UserLogin");
        }
        public IActionResult Mytour()
        {
            ViewBag.User = HttpContext.Session.GetString("name");
            string ten = HttpContext.Session.GetString("name");
            var tk = this.tourReContext.User.Where(x => x.Username.Equals(ten)).FirstOrDefault();
            var mytour = this.tourReContext.Dattour.Where(x=>x.Hoten.Equals(tk.Name)).FirstOrDefault();
            ViewBag.MyTour = this.tourReContext.Dattour.Where(x => x.Hoten.Equals(tk.Name)).ToList();
            return View(mytour);
        }
        public IActionResult ForgotPass()
        {
            
            return View();
        }
        public IActionResult Deletetour()
        {
            try
            {
                string tenkh = HttpContext.Session.GetString("name");
                var tk = this.tourReContext.User.Where(x => x.Username.Equals(tenkh)).FirstOrDefault();
                //string tentour = Request.Form["tentour"];
                var deltour = this.tourReContext.Dattour.Where(x => x.Hoten.Equals(tk.Name)).FirstOrDefault();
                this.tourReContext.Dattour.Remove(deltour);
                this.tourReContext.SaveChanges();
            }
            catch(Exception e)
            {

            }
            return RedirectToAction("Mytour", "UserLogin");
        }
        public IActionResult Email()
        {
            ViewData["Message"] = "ASP.NET Core mvc send email example";
            string email = Request.Form["Email"];
            var tk = this.tourReContext.User.Where(x => x.Email.Equals(email)).FirstOrDefault();
            // _EmailService.Send("hai98988989@gmail.com", "ASP.NET Core mvc send email example", "Send from asp.net core mvc action");
            //SendMailLocalSmtp("trandanghai2017603599@gmail.com", "hai98988989@gmail.com", "Chủ đề", "Nội dung email").Wait();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Hai", "trandanghai2017603599@gmail.com"));

            message.To.Add(new MailboxAddress("Custommer", email));

            message.Subject = "Your PassWord!";

            message.Body = new TextPart("plain")
            {
                Text = tk.Pass
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587,false);
                client.Authenticate("trandanghai2017603599@gmail.com", "hai18081999");
                client.Send(message);
                client.Disconnect(true);
            }
            return RedirectToAction("Index","UserLogin");
        }

        }
    }
