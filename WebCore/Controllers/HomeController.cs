using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCore.Filter;
using WebCore.Model;
using WebCore.Services;

namespace WebCore.Controllers
{
    public class HomeController : Controller
    {
        private IPersonService _personService;
        private MyContext _context;


        public HomeController(IPersonService personService, MyContext context)
        {
            _personService = personService;
            _context = context;
        }

        [ResourceFilter]
        [ActionFilter]
        [ExceptionFilter]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Index()
        {
            //throw new Exception("错误");
            var str = _personService.Add();
            ViewBag.str = str;

            Person p = new Person() { Name = "xbh", Age = 20 };
            _context.Add(p);
            _context.SaveChanges();
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]Person person)
        {
            return View();
        }

        public IActionResult Login()
        {
            return Content("login");
        }

        public IActionResult DoLogin()
        {

            string token = "123456";
            string name = "xbh";
            ClaimsIdentity identity = new ClaimsIdentity("Forms");
            identity.AddClaim(new Claim(ClaimTypes.Sid, token));
            identity.AddClaim(new Claim(ClaimTypes.Name, name));

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Content("登录成功");
        }


        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Center()
        {
            var token = User.FindFirstValue(ClaimTypes.Sid);
            return Content("center");
        }

    }
}