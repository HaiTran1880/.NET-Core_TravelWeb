using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace TTTN_Travel
{
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.Extensions.FileProviders;
    using NETCore.MailKit.Extensions;
    using NETCore.MailKit.Infrastructure.Internal;
    using System.IO;
    using TTTN_Travel.Middlewares;
    using TTTN_Travel.Models;
    using TTTN_Travel.Models.Global;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //Add to use Session
            services.AddSession();
            //Add to use MVC
            services.AddMvc();
            services.AddOptions();                                         // Kích hoạt Options
            var mailsettings = Configuration.GetSection("MailSettings");  // đọc config
            services.Configure<MailSettings>(mailsettings);
            /*//Add MailKit
            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new MailKitOptions()
                {
                    //get options from sercets.json
                    Server = Configuration["Server"],
                    Port = Convert.ToInt32(Configuration["Port"]),
                    SenderName = Configuration["Hai Tran"],
                    SenderEmail = Configuration["trandanghai2017603599@gmail.com"],

                    //can be optional with no authentication 
                    Account = Configuration["trandanghai2017603599@gmail.com"],
                    Password = Configuration["hai18081999"]
                });
            });*/
            //Add MailKit
            services.AddMailKit(optionBuilder =>
            {
                optionBuilder.UseMailKit(new MailKitOptions()
                {
                    //get options from sercets.json
                    Server = Configuration["Server"],
                    Port = Convert.ToInt32(Configuration["Port"]),
                    SenderName = Configuration["SenderName"],
                    SenderEmail = Configuration["SenderEmail"],

                    //can be optional with no authentication 
                    Account = Configuration["Account"],
                    Password = Configuration["Password"]
                });
            });
            //services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            //services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
            //cfg.Cookie.Name = "hai";                    // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
            //cfg.IdleTimeout = new TimeSpan(0, 60, 0);   // Thời gian tồn tại của Session
            //});

            var connectionString = Configuration.GetConnectionString("TravelConnection");
            services.AddDbContext<TourReContext>(item => item.UseSqlServer(connectionString));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

           /* services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders =
            ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);*/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseSession();// Đăng ký Middleware Session vào Pipeline
            //add sercets.json https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets
           
            //use Session
            //app.UseSession();
            //use Middleware
            app.UseMiddleware<CheckLoginMiddleware>();
            //use folder wwwroot
            //app.UseStaticFiles();

            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseMvc();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                     Path.Combine(Directory.GetCurrentDirectory(), @"Content/images")),
                RequestPath = new PathString("/images")
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

        }
    }
}
