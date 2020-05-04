using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_2020_Business.Models;
using MVC_2020_Business.Services;
using MVC_2020_Template.Models;
using Newtonsoft.Json;

namespace MVC_2020_Template.Controllers
{
    public class SecureController : Controller
    {
        private readonly IHostingEnvironment _env;
        public SecureController(IHostingEnvironment env)
        {
            _env = env;
        }
        // GET: Secure
        public ActionResult Auto()
        {
            if (new Helpers.Authentication(HttpContext, _env).Success)
            {
                ViewBag.isLoggedin = true;
            }
            else
            {
                ViewBag.isLoggedin = false;
            }
            return PartialView();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.isLoggedin = false;
            return PartialView("Auto");
        }

    }
}
