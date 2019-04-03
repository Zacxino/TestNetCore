using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using WebCore.Filter;
using WebCore.Model;
using WebCore.Services;

namespace WebCore
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //services.AddMvc(o => //全局注册Filter
            //{
            //    o.Filters.Add(typeof(ExceptionFilter));
            //});

            //授权过滤器 Authorize
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, f => {
                f.LoginPath = new PathString("/Home/Login");
            });

            //IOC 
            services.AddTransient<IPersonService, PersonService>();

            //配置文件
            services.Configure<PersonConfig>(_configuration.GetSection(nameof(PersonConfig)));

            //EFCore  
            services.AddDbContext<MyContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //使用静态文件
            //app.UseStaticFiles(new StaticFileOptions() {
            //    FileProvider = new  PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Images"))
            //});

            app.UseStaticFiles();

            //Authorize
            app.UseAuthentication();

            //使用mvc默认路由
            app.UseMvcWithDefaultRoute();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
