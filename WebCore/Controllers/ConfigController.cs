using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WebCore.Controllers
{
    public class ConfigController : Controller
    {
        private PersonConfig _personConfig;

        public ConfigController(IOptions<PersonConfig> option)
        {
            _personConfig = option.Value;
        }

        public IActionResult Index()
        {
            //ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            //configurationBuilder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            //configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json");

            //var config = configurationBuilder.Build();

            //var str = config.GetValue<string>("AllowedHosts");
            //var str = config["Logging:LogLevel:Default"];


            var str = _personConfig.Name;

            return Content(str);
        }
    }
}