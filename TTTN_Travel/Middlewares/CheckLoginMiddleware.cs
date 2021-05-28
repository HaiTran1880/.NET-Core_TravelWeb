using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TTTN_Travel.Models;
using TTTN_Travel.Models.Global;

namespace TTTN_Travel.Middlewares
{
    public class CheckLoginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TourReContext tourReContext = new TourReContext();
        public CheckLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        private string GetIPAddress()
        {
            var IPAddress = "";
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }

        private async Task abc(HttpContext httpContext)
        {
            httpContext.Session.SetString("Online", GetIPAddress());
            var st =tourReContext.Statistical.Where(x => x.ID == 1).FirstOrDefault();
            st.Visit = st.Visit + 1;
            int a = st.Visit;
            Online.online += 1;
            tourReContext.Statistical.Update(st);
            tourReContext.SaveChanges();
        }

        public Task Invoke(HttpContext httpContext)
        {
            ////Lấy đường dẫn url
            var path = httpContext.Request.Path.ToString();
            //nếu path bắt đầu bằng chữ/admin thì kiểm tra xem session đã tồn tại chưa => nếu chưa tồn tại thì di chuyển đến trang login
            path = path.ToLower();
            if (path != null && path.StartsWith("/admin"))
            {
                if (httpContext.Session.GetString("Account") == null)
                {
                    //di chuyển đến trang login
                    httpContext.Response.Redirect("/Login/Index");
                }
                   
            }
            else if (path != null && !path.Equals("/admin") && !path.Equals("/Login/Index"))
            {
               
                //Thống kê người ghé vào web
                if (httpContext.Session.GetString("Online") != GetIPAddress())
                {
                    abc(httpContext);
                }
           }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CheckLoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseCheckLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckLoginMiddleware>();
      }
    }
}
