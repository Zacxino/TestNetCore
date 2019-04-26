using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NetCore2_2.MVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger, IServer server)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //如果应用程序应覆盖每个请求的MaxRequestBodySize，请使用IHttpMaxRequestBodySizeFeature：
            //app.Use(async (context, next) =>
            //{
            //    context.Features.Get<IHttpMaxRequestBodySizeFeature>()
            //        .MaxRequestBodySize = 10 * 1024;

            //    var serverAddressesFeature =
            //        app.ServerFeatures.Get<IServerAddressesFeature>();
            //    var addresses = string.Join(", ", serverAddressesFeature?.Addresses);

            //    logger.LogInformation($"Addresses: {addresses}");

            //    await next.Invoke();
            //});

            //注册middleware Run注册终结点
            app.Run(async (HttpContext context) =>
            {
                var process = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                logger.LogInformation($"ProcessName: {process}");
                await context.Response.WriteAsync("Hello Net Core " + process);
            });

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
