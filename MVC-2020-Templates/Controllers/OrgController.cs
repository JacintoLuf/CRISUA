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

        private string iupi = "66c74f1f-8c45-4f43-9a85-be4975eecc09";
        private string nome = "José Manuel Neto Vieira";
        private string orcid = "0000-0002-4356-4522";

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
        public IActionResult Index()
        {
            //var tempOrgUnit = _db.OrgUnit.ToList();
            var person = _db.Person_Identifier.FirstOrDefault(x => x.Value.Equals(iupi/*Session.IUPI.ToString()*/));
            var personId = 0;
            var associatedOrgUnits = new List<Person_OrgUnit>();
            if (person != null)
            {
                personId = person.PersonID;
                var tempOrgUnits = _db.Person_OrgUnit.ToList();
                foreach (var a in tempOrgUnits)
                {
                    if (a.PersonID == personId)
                    {
                        associatedOrgUnits.Add(a);
                    }

                }
            }
            var listOrgUnit = new List<UnidadeInvestigacao>();
            var listAddresses = DatabaseServices.getAddresses(_db);
            foreach (var temp in associatedOrgUnits)
            {

                listOrgUnit.Add(DatabaseServices.retrieveInfoUI(_db, temp.OrgUnitId));

            }
            ViewBag.OrgUnits = listOrgUnit.ToList();
            ViewBag.Adresses = listAddresses;
            return View();
        }

        [HttpPost]
        public IActionResult Index(string cancelar_bt, string adicionar_bt, int org, DateTime data_ini, DateTime data_fim)
        {
            //Guarda dados editados na BD
            if (adicionar_bt == "add")
            {
                //var id = @Newtonsoft.Json.JsonConvert.DeserializeObject<UnidadeInvestigacao>(org).OrgUnitId;
               // DatabaseServices.AssociatePersonOrgUnit(_db, org, data_ini, data_fim, iupi/*Session.IUPI.ToString()*/);

            }
            return RedirectToAction("Index", "Org");
        }

        public IActionResult Details(String org)
        {
            ViewBag.Address = DatabaseServices.getAddresses(_db).Find(x => x.PAddressId == @Newtonsoft.Json.JsonConvert.DeserializeObject<UnidadeInvestigacao>(org).PAddressId);
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(org);
            return View();
        }
           
        public IActionResult Details_Helper(String org)
        {
            //ViewBag.Address = DatabaseServices.getAddresses(_db).Find(x => x.PAddressId == @Newtonsoft.Json.JsonConvert.DeserializeObject<UnidadeInvestigacao>(org).PAddressId);
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(org);
            return Json(Url.Action("Details", "Org", new { org = org }));
        }
    }
}
