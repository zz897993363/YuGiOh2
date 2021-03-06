using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoonSharp.Interpreter;
using System;
using System.Reflection;
using YuGiOh2.Data;
using YuGiOh2.Hubs;

namespace YuGiOh2
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
            var mysqlString = Configuration.GetConnectionString("MySqlConnection");
            services.AddDbContextPool<DBContext>(options => options.UseMySql(mysqlString));
            //DuelUtils.DBContext = services.BuildServiceProvider().GetService<DBContext>();
            DuelUtils.Builder = new DbContextOptionsBuilder<DBContext>().UseMySql(mysqlString);
            services.AddSingleton<DbContext, DBContext>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSignalR();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DuelUtils.ScriptsPath = @"G:\ygo2\YuGiOh2";
            }
            else
            {
                DuelUtils.ScriptsPath = env.ContentRootPath + "/Scripts/";
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            UserData.RegisterAssembly(assembly);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseEndpoints(ep =>
            {
                ep.MapHub<DuelHub>("/hub");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
