using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVC_2020_Business.Models;
using MVC_2020_Business.Services;
using MVC_2020_Template.Helpers;
using MVC_2020_Template.Models;
using Newtonsoft.Json;

namespace MVC_2020_Template.Controllers
{
    public class SecureController : Controller
    {
        private readonly IHostingEnvironment _env;
        public readonly IConfiguration _conf;
        public SecureController(IHostingEnvironment env, IConfiguration conf)
        {
            _env = env;
            _conf = conf;
        }
        // GET: Secure
        public ActionResult Index()
        {
            new Authentication(HttpContext, _env, _conf);
            //var serverVars = HttpContext.Features.Get<IServerVariablesFeature>();
            //var iupiSession = serverVars?["iupi"]?.ToString();
            //var emailSession = serverVars?["mail"]?.ToString();
            //ViewBag.iupiSession = iupiSession;
            //ViewBag.emailSession = emailSession;

            ViewBag.isLoggedin = Session.SessionActive;
            
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            Session.Logout();
            return Redirect("/secure/Shibboleth.sso/Logout");
        }
    }
}
