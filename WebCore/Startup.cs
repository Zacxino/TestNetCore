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
using WebCore.Middleware;
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

            //注册middleware Run注册终结点
            //app.Run(async (HttpContext context) =>
            //{
            //    await context.Response.WriteAsync("Hello Asp.Net Core");
            //});

            //use 写法一：
            //use 表示注册动作 不是终结点
            //执行next，就可以执行下一个中间件 如果不执行 就等于Run
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello Use");
            //    await next();
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello Use again");
            //    await next();
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello Use again again");
            //    await next();
            //});

            //use 写法二： 中间件 
            app.UseMiddleware<MyMiddleWare>();

            //map:可以根据条件指定中间件 分发请求的
            //app.Map("/test", MapTest);
            //app.MapWhen(context =>
            //{
            //    return context.Request.Query.ContainsKey("a");
            //}, MapTest);


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

        private static void MapTest(IApplicationBuilder app)
        {
            //app.Run(async (HttpContext context) =>
            //{
            //    await context.Response.WriteAsync("Hello Map");
            //});

            RequestDelegate request = new RequestDelegate(Ctx);
            app.Run(request);
        }

        

        public async static Task Ctx(HttpContext context)
        {
            await context.Response.WriteAsync("Hello Map");
        }
    }
}
