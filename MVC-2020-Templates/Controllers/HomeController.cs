﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
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
        private static MyDbContext _db;// acesso db

        public HomeController(ILogger<HomeController> logger, MyDbContext db)// acesso db
        {
            _logger = logger;
            _db = db;// acesso db
        }

        public static MyDbContext DB()
        {
            return _db;
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
            //ViewBag.PublicacoesRIA = PublicacoesService.GetProducts(_db, Session.IUPI.ToString());
            //ViewBag.PublicacoesOrcid = PublicacoesService.GetWorksFromXml();
            ViewBag.PublicacoesPTCris = PublicacoesService.GetDifWorks(_db,
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
            //ViewBag.Details = JsonConvert.DeserializeObject<List<Work>>(works);
            ViewBag.Details = MVC_2020_Business.Services.DatabaseServices.select(_db, "Publication", "Synced", "1");
            ViewBag.PublicacoesPTCris2 = PublicacoesService.GetDifWorks2(_db,
                                        PublicacoesService.ConvertProductToWork(
                                        PublicacoesService.GetProducts2(_db, Session.IUPI.ToString())));
            return View();
            //return Json(Url.Action("Detail", "Home"));
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

        public IActionResult Home()
        {
            return View();
        }
        public IActionResult PublicationMetaData1()
        {
            return View();
        }

        public IActionResult PublicationMetaData2()
        {
            return View();
        }

        public IActionResult PublicationMetaData3()
        {
            return View();
        }

        public IActionResult PublicationDoiSearch()
        {
            return View();
        }

        public IActionResult PublicationSubmission()
        {
            return View();
        }

        public IActionResult dSPaceLicense()
        {
            return View();
        }

        public IActionResult Publication_Details()
        {
            return View();
        }

        public IActionResult Sobre()
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
            MVC_2020_Business.Services.DatabaseServices.updateState2(_db, works, 1);
            return Json(Url.Action("Detail", "Home"));
        }
    }
}