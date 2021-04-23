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
    using Microsoft.Extensions.FileProviders;
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
            //services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            //services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
                //cfg.Cookie.Name = "hai";                    // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                //cfg.IdleTimeout = new TimeSpan(0, 60, 0);   // Thời gian tồn tại của Session
            //});

            var connectionString = Configuration.GetConnectionString("TravelConnection");
            services.AddDbContext<TourReContext>(item => item.UseSqlServer(connectionString));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseSession();// Đăng ký Middleware Session vào Pipeline

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
