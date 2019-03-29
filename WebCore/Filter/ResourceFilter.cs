using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebCore.Filter
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public class ResourceFilter : Attribute,IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //context.HttpContext.User.FindFirstValue(ClaimTypes.Sid);//这里获取不到token

            //context.Result = new ContentResult() { Content = "结束执行" };
        }
    }
}
