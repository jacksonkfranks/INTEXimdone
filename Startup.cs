using Microsoft.AspNetCore.Authentication.Cookies;
using INTEXimdone.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime;
using INTEXimdone.Data;

namespace INTEXimdone
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })

                .AddCookie(options =>
                {
                    options.LoginPath = "/account/google-login";
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "87288611609-lvn3pfpgcc5rtnsg8ct3t520n0gilhcu.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddDbContext<CrashDbContext>(options =>
            
                options.UseMySql(Configuration.GetConnectionString("CrashDataDbConnection"))
            );

            services.AddDbContext<ApplicationDbContext>(options =>

                options.UseMySql(Configuration.GetConnectionString("CrashDataDbConnection"))
            );

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<ICrashRepository, EFCrashRepository>();
            services.AddServerSideBlazor();

            services.AddSingleton<InferenceSession>(
                new InferenceSession("Models/utah_crash_severity.onnx")
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "Paging",
                    "Page{pageNum}",
                    new { Controller = "Home", action = "Temp", pageNum = 1 });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Calculator",
                    pattern: "{controller=Inference}/{action=Calculator}");

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}", "/admin/Index");
            });
        }
    }
}
