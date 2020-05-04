using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_2020_Business.Models;
using MVC_2020_Business.Services;
using MVC_2020_Template.Models;
using Newtonsoft.Json;

namespace MVC_2020_Template.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MVC_2020_Database.DataModels.MyDbContext _db;// acesso db

        public HomeController(ILogger<HomeController> logger, MVC_2020_Database.DataModels.MyDbContext db)// acesso db
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
            ViewBag.PublicacoesRIA = PublicacoesService.GetProducts(_db);
            ViewBag.PublicacoesOrcid = PublicacoesService.GetWorksFromXml();
            ViewBag.PublicacoesPTCris = PublicacoesService.GetDifWorks(
                                        PublicacoesService.ConvertProductToWork(
                                        PublicacoesService.GetProducts(_db)));
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