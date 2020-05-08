using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_2020_Business.Models;
using MVC_2020_Business.Services;
using MVC_2020_Database.DataModels;
using MVC_2020_Template.Helpers;
using MVC_2020_Template.Models;
using Newtonsoft.Json;

namespace MVC_2020_Template.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _db;// acesso db

        public HomeController(ILogger<HomeController> logger, MyDbContext db)// acesso db
        {
            _logger = logger;
            _db = db;// acesso db
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            return View();
        }

        public IActionResult Publicacoes()
        {
            ViewBag.PublicacoesRIA = PublicacoesService.GetProducts(_db, Session.IUPI.ToString());
            ViewBag.PublicacoesOrcid = PublicacoesService.GetWorksFromXml();
            ViewBag.PublicacoesPTCris = PublicacoesService.GetDifWorks(
                                        PublicacoesService.ConvertProductToWork(
                                        PublicacoesService.GetProducts(_db, Session.IUPI.ToString())));
            return View();
        }

        public IActionResult Content()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Errors()
        {
            return View();
        }

        public IActionResult Forms()
        {
            return View();
        }

        public IActionResult Readme()
        {
            return View();
        }

        public IActionResult Extra()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SavePub(String works)
        {
            var w = JsonConvert.DeserializeObject<List<Work>>(works);
            return Json("Foram inseridas as publicações no RIA \n\nAcabou por hoje! :)");
        }
    }
}