using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TestCore.Middleware
{
    public class MyMiddleWare
    {
        private readonly RequestDelegate _next;

        public MyMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        // public Task Invoke(HttpContext context)
        // {
        //     context.Response.WriteAsync("Hello MyMiddleWare");
        //     // Call the next delegate/middleware in the pipeline
        //     return this._next(context);
        // }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                {
                    if (context.Request.Path.Value.EndsWith(".map"))
                    {
                        //context.Response.ContentType = "application/json";
                        context.Response.WriteAsync("Hello MyMiddleWare");
                    }
                }

                return Task.FromResult(0);
            });
            await this._next(context);
        }
    }
}
