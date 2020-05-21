using System;
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

        [HttpGet]
        public IActionResult Perfil()
        {
            //Estou a passar o IUPI do Vieira pãra testar
            var aux = MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, "66c74f1f-8c45-4f43-9a85-be4975eecc09"/*Session.IUPI.ToString()*/);  //IR buscar o Orcid ID à BD
            if (aux != null)
            {
                ViewBag.OrcidID = aux;
            }
            else {
                ViewBag.OrcidID = null;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Perfil(String OrcidID)
        {
            MVC_2020_Business.Services.DatabaseServices.setOrcid(_db, Session.IUPI.ToString(), OrcidID); //meter o OrcidID na BD
            //ViewBag.OrcidID = "0000-0002-3488-6570";
            ViewBag.OrcidID = MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, Session.IUPI.ToString());
            return View();
        }
        public IActionResult MyPublications()
        {
            ViewBag.pubsInBD = MVC_2020_Business.Services.DatabaseServices.selectToRIA(_db, MVC_2020_Business.Services.DatabaseServices.selectAllPubsInBD(_db, "Publication", "State"));
            return View();
        }

        [HttpGet]
        public IActionResult Publicacoes()
        {
            //ViewBag.PublicacoesRIA = PublicacoesService.GetProducts(_db, Session.IUPI.ToString());
            //ViewBag.PublicacoesOrcid = PublicacoesService.GetWorksFromXml();
            ViewBag.worksInBD = MVC_2020_Business.Services.DatabaseServices.selectToRIA(_db, MVC_2020_Business.Services.DatabaseServices.select(_db, "Publication", "State", "1"));
            //ViewBag.PublicacoesPTCris = PublicacoesService.GetDifWorks(_db,
            //                            PublicacoesService.ConvertProductToWork(
            //                            PublicacoesService.GetProducts(_db, Session.IUPI.ToString())));
            ViewBag.import = "false";
            return View();
        }
        
        [HttpPost]
        public IActionResult Publicacoes(string import)
        {
            //ViewBag.PublicacoesRIA = PublicacoesService.GetProducts(_db, Session.IUPI.ToString());
            //ViewBag.PublicacoesOrcid = PublicacoesService.GetWorksFromXml();
            ViewBag.PublicacoesPTCris = PublicacoesService.GetDifWorks(_db, Session.FullName,
                                        PublicacoesService.ConvertProductToWork(
                                        PublicacoesService.GetProducts(_db, Session.IUPI.ToString())));
            ViewBag.worksInBD = MVC_2020_Business.Services.DatabaseServices.selectToRIA(_db, MVC_2020_Business.Services.DatabaseServices.select(_db, "Publication", "State", "1"));
            
            ViewBag.import = import;
            return View();
        }

        public IActionResult Content()
        {
            return View();
        }

        public IActionResult PublicacoesSalvas(string works)
        {
            //ViewBag.Details = JsonConvert.DeserializeObject<List<Work>>(works);
            //ViewBag.Titulos = MVC_2020_Business.Services.DatabaseServices.select(_db, "Publication", "Synced", "1");
            ViewBag.PubSaved = MVC_2020_Business.Services.DatabaseServices.selectToRIA(_db, MVC_2020_Business.Services.DatabaseServices.select(_db, "Publication", "State", "2"));
            //ViewBag.PublicacoesPTCris2 = PublicacoesService.GetDifWorks2(_db,
            //                            PublicacoesService.ConvertProductToWork(
            //                            PublicacoesService.GetProducts2(_db, Session.IUPI.ToString())));
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

        public IActionResult PublicationMetaData1(String works)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return View();
        }

        public IActionResult PublicationMetaData1_Help(String works)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return Json(Url.Action("PublicationMetaData1", "Home", new { works = works }));
        }

        
        public IActionResult PublicationDoiSearch()
        {
            return View();
        }
                
        public IActionResult PublicationMetaData2(String works)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return View();
        }

        public IActionResult PublicationMetaData2_Helper(String works)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return Json(Url.Action("PublicationMetaData2", "Home", new { works = works }));
        }

        public IActionResult PublicationMetaData3(String works)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return View();
        }

        public IActionResult PublicationMetaData3_Helper(String works)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return Json(Url.Action("PublicationMetaData3", "Home", new { works = works }));
        }

        public IActionResult PublicationSubmission(String works)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return View();
        }

        public IActionResult PublicationSubmission_Helper(String works)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return Json(Url.Action("PublicationSubmission", "Home", new { works = works }));
        }

        public IActionResult dSPaceLicense(String works)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return View();
        }

        public IActionResult dSPaceLicense_Helper(String works)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return Json(Url.Action("dSPaceLicense", "Home", new { works = works }));
        }

        public IActionResult Publication_Details(String obj)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(obj);
            return View();
        }

        public IActionResult PublicationDetails_Helper(String obj)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(obj);
            return Json(Url.Action("Publication_Details", "Home", new { obj = obj }));
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
        public IActionResult SavePub(string works)
        {
            List<string> titulos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(works);
            foreach(string t in titulos)
            {
                if (t != null) MVC_2020_Business.Services.DatabaseServices.updateState(_db, t, 2);
            }
            return Json(Url.Action("PublicacoesSalvas", "Home"));
        }

        [HttpPost]
        public IActionResult BackToSavedPubs(String works)
        {
            return Json(Url.Action("PublicacoesSalvas", "Home"));
        }
    }
}