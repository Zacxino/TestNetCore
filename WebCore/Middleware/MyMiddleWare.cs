using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Middleware
{
    public class MyMiddleWare
    {
        private readonly RequestDelegate _next;

        public MyMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.WriteAsync("Hello MyMiddleWare");
            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }
}
