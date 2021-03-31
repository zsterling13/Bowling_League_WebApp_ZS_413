using Bowling_League_WebApp_ZS_413.Models;
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

namespace Bowling_League_WebApp_ZS_413
{
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
            services.AddControllersWithViews();

            //Add a service that connects with the sqlite database through the appsettings.json file
            services.AddDbContext<BowlingLeagueContext>(options =>
               options.UseSqlite(Configuration["ConnectionStrings:BowlingDbConnection"])
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Initial Endpoint to display as much info as possible when a page link is selected
                endpoints.MapControllerRoute("teamNamePageNum",
                    "teamName/{teamID}/{teamName}/{pageNum}",
                    new { Controller = "Home", action = "Index" }
                    );

                //Secondary endpoint that then tries to show data of the filtered team when a page number has not been selected
                endpoints.MapControllerRoute("teamID",
                    "teamfilter/{teamID}/{teamName}",
                    new { Controller = "Home", action = "Index" }
                    );

                //Third endpoint that only displays the page number when no other data is available
                endpoints.MapControllerRoute(
                    "pagenum",
                    "All/{pagenum}",
                    new { Controller = "Home", action = "Index" }
                    );

                //Default and last-resort endpoint
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
