using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Filter
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public class ActionFilter : ActionFilterAttribute
    {
        public ActionFilter()
        {
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            //context.Result = new ContentResult() { Content = "没有权限" };
        }
    }
}
