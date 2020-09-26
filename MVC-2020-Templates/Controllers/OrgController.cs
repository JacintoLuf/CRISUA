using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_2020_Business.Models;
using MVC_2020_Business.Services;
using MVC_2020_Database.DataModels;

namespace MVC_2020_Template.Controllers
{
    public class OrgController : Controller
    {
        private readonly ILogger<OrgController> _logger;
        private static MyDbContext _db;// acesso db
        private IHostingEnvironment _hostingEnvironment;

        public OrgController(ILogger<OrgController> logger, MyDbContext db, IHostingEnvironment hosting)// acesso db
        {
            _logger = logger;
            _db = db;// acesso db
            _hostingEnvironment = hosting;
        }

        public static MyDbContext DB()
        {
            return _db;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Index(int orgUnitId)
        {
            var tempOrgUnit = _db.OrgUnit.ToList();
            var listOrgUnit = new List<UnidadeInvestigacao>();
            foreach (var temp in tempOrgUnit)
            {

                listOrgUnit.Add(DatabaseServices.retrieveInfoUI(_db, temp.OrgUnitId));

            }
            ViewBag.OrgUnits = listOrgUnit.ToList();
            //ViewBag.OrgUnits = DatabaseServices.retrieveInfoUI(_db,1);
            return View();
        }

        public IActionResult Details(String obj)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(obj);
            return View();
        }

        
    }
}