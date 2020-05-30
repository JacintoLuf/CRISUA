﻿using System;
using System.Collections;
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
using ServiceStack;

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

        [HttpPost]
        public IActionResult PublicationMetaData1(string works, string submit_next, string submit_cancel,string dc_contributor_author, string dc_title,
                                                    string dc_title_alternative, string degois_publication_title /*revista*/, string dc_date_issued_day,
                                                    string dc_date_issued_month, string dc_date_issued_year, string degois_publication_volume, string degois_publication_issue,
                                                    string degois_publication_firstPage, string degois_publication_lastPage,string dc_publisher, string dc_identifier_issn,
                                                    string dc_identifier_essn, string dc_identifier_doi, string dc_peerreviewed, string dc_relation_publisherversion,
                                                    string dc_type, string dc_description_version, string dc_language_iso)
        {
            Hashtable dados = publicationMeta1ToHash(dc_contributor_author, dc_title, dc_title_alternative,  degois_publication_title /*revista*/,  dc_date_issued_day,
                                                     dc_date_issued_month,  dc_date_issued_year,  degois_publication_volume,  degois_publication_issue,
                                                     degois_publication_firstPage,  degois_publication_lastPage,  dc_publisher,  dc_identifier_issn,
                                                     dc_identifier_essn,  dc_identifier_doi,  dc_peerreviewed,  dc_relation_publisherversion,
                                                     dc_type,  dc_description_version,  dc_language_iso);

            if (submit_next == "Next")
            {
                //Console.WriteLine("Titulo: " + dc_title);
                //Console.WriteLine("Autores: " + dc_contributor_author);
                return RedirectToAction("PublicationMetaData2", "Home", new { works = works });
            }
            if (submit_cancel == "Cancel/Save")
                return RedirectToAction("PublicacoesSalvas", "Home");

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

        [HttpPost]
        public IActionResult PublicationMetaData2(string works, string submit_next, string submit_cancel, string submit_prev, string submit_jump_2_1,
                                                    string dc_subject_1 /*palavra chave*/, string dc_description_abstract_1 /*resumo*/, string dc_relation_authority, /*prof financiado*/
                                                    string dc_description_sponsorship /*patrocinadores*/, string dc_rights /*acesso*/,
                                                    string dc_date_embargo_day, string dc_date_embargo_month, string dc_date_embargo_year, string dc_rights_uri /*licensa*/)
        {
            Hashtable dados = publicationMeta2ToHash(dc_subject_1 /*palavra chave*/, dc_description_abstract_1 /*resumo*/, dc_relation_authority, /*prof financiado*/
                                                     dc_description_sponsorship /*patrocinadores*/, dc_rights /*acesso*/,
                                                     dc_date_embargo_day, dc_date_embargo_month, dc_date_embargo_year, dc_rights_uri /*licensa*/);

            if (submit_jump_2_1 == "Describe" || submit_prev == "Previous")
                return RedirectToAction("PublicationMetaData1", "Home", new { works = works });
            if (submit_next == "Next")
            {
                return RedirectToAction("PublicationMetaData3", "Home", new { works = works });
            }
            if (submit_cancel == "Cancel/Save")
                return RedirectToAction("PublicacoesSalvas", "Home");

            return View();
        }

        public IActionResult PublicationMetaData2_Helper(String works, string dc_title)
        {
            Console.WriteLine("Titulo: " + dc_title);
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return Json(Url.Action("PublicationMetaData2", "Home", new { works = works }));
        }

        public IActionResult PublicationMetaData3(String works)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return View();
        }

        [HttpPost]
        public IActionResult PublicationMetaData3(string works, string submit_next, string submit_cancel, string submit_prev, string submit_jump_2_1, string submit_jump_2_2)
        {
            if (submit_jump_2_1 == "Describe")
                return RedirectToAction("PublicationMetaData1", "Home", new { works = works });
            if (submit_jump_2_2 == "Describe" || submit_prev == "Previous")
                return RedirectToAction("PublicationMetaData2", "Home", new { works = works });
            if (submit_next == "Next")
            {
                return RedirectToAction("PublicationSubmission", "Home", new { works = works });
            }
            if (submit_cancel == "Cancel/Save")
                return RedirectToAction("PublicacoesSalvas", "Home");

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

        [HttpPost]
        public IActionResult PublicationSubmission(string works, string submit_next, string submit_cancel, string submit_prev, string submit_jump_2_1, string submit_jump_2_2, string submit_jump_3_1
                                                    )
        {
            if (submit_jump_2_1 == "Describe" || submit_jump_2_1 == "Correct one of these")
                return RedirectToAction("PublicationMetaData1", "Home", new { works = works });

            if (submit_jump_2_2 == "Describe" || submit_jump_2_2 == "Correct one of these")
                return RedirectToAction("PublicationMetaData2", "Home", new { works = works });

            if (submit_jump_3_1 == "Upload" || submit_prev == "Previous" || submit_jump_3_1 == "Add or Remove a File")
                return RedirectToAction("PublicationMetaData3", "Home", new { works = works });

            if (submit_next == "Next")
            {
                return RedirectToAction("dSpaceLicense", "Home", new { works = works });
            }
            if (submit_cancel == "Cancel/Save")
                return RedirectToAction("PublicacoesSalvas", "Home");

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

        [HttpPost]
        public IActionResult dSPaceLicense(string works, string submit_reject, string submit_grant, string submit_jump_2_1, string submit_jump_2_2, string submit_jump_3_1, string submit_jump_4_1)
        {
            if (submit_jump_2_1 == "Describe")
                return RedirectToAction("PublicationMetaData1", "Home", new { works = works });

            if (submit_jump_2_2 == "Describe")
                return RedirectToAction("PublicationMetaData2", "Home", new { works = works });

            if (submit_jump_3_1 == "Upload")
                return RedirectToAction("PublicationMetaData3", "Home", new { works = works });

            if (submit_jump_4_1 == "Verify")
                return RedirectToAction("PublicationSubmission", "Home", new { works = works });

            if (submit_reject == "I Do Not Grant the License")
            {
                return RedirectToAction("Home", "Home", new { works = works });
            }

            if (submit_grant == "I Grant the License")
            {
                return RedirectToAction("Home", "Home", new { works = works });
            }

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

        public ActionResult Singlelogout()
        {
            HttpContext.Session.Clear();
            string returnTo = HttpContext.Request.Query["return"].ToString();
            if (!string.IsNullOrEmpty(returnTo))
                return Redirect(returnTo);
            return Redirect("Home");
        }

        public static Hashtable publicationMeta1ToHash(string dc_contributor_author, string dc_title,
                                                    string dc_title_alternative, string degois_publication_title /*revista*/, string dc_date_issued_day,
                                                    string dc_date_issued_month, string dc_date_issued_year, string degois_publication_volume, string degois_publication_issue,
                                                    string degois_publication_firstPage, string degois_publication_lastPage, string dc_publisher, string dc_identifier_issn,
                                                    string dc_identifier_essn, string dc_identifier_doi, string dc_peerreviewed, string dc_relation_publisherversion,
                                                    string dc_type, string dc_description_version, string dc_language_iso)
        {
            Hashtable temp = new Hashtable();
            temp.Add("titulo", dc_title);
            List<string> nomes = dc_contributor_author.Split(";").ToList();
            temp.Add("Autores", nomes);
            temp.Add("titulo alternativo", dc_title_alternative);
            temp.Add("Revista", degois_publication_title);
            temp.Add("Dia", dc_date_issued_day);
            temp.Add("Mes", dc_date_issued_month);
            temp.Add("Ano", dc_date_issued_year);
            temp.Add("Volume", degois_publication_volume);
            temp.Add("Edicao", degois_publication_issue);
            temp.Add("StartPage", degois_publication_firstPage);
            temp.Add("EndPage", degois_publication_lastPage);
            temp.Add("Editor", dc_publisher);
            temp.Add("ISSN", dc_identifier_issn);
            temp.Add("E-ISSN", dc_identifier_essn);
            temp.Add("DOI", dc_identifier_doi);
            temp.Add("Peer Review", dc_peerreviewed);
            temp.Add("PublisherVersion", dc_relation_publisherversion);
            temp.Add("Tipo", dc_type);
            temp.Add("Estado", dc_description_version);
            temp.Add("Idioma", dc_language_iso);

            //Console.WriteLine("HASHTABLE AUTORES: ");
            //foreach (var d in (List<string>)temp["Autores"])
            //{
            //    Console.WriteLine(d);
            //}
                

            return temp;
        }

        public static Hashtable publicationMeta2ToHash(string dc_subject_1 /*palavra chave*/, string dc_description_abstract_1 /*resumo*/, string dc_relation_authority, /*prof financiado*/
                                                        string dc_description_sponsorship /*patrocinadores*/, string dc_rights /*acesso*/, string dc_date_embargo_day, 
                                                        string dc_date_embargo_month, string dc_date_embargo_year, string dc_rights_uri /*licensa*/)
        {
            Hashtable temp = new Hashtable();

            temp.Add("Palavra-Chave", dc_subject_1);
            temp.Add("Resumo", dc_description_abstract_1);
            temp.Add("Financiamento", dc_relation_authority);
            temp.Add("Patrocinadores", dc_description_sponsorship);
            temp.Add("Acesso", dc_rights);
            temp.Add("Dia-Embargo", dc_date_embargo_day);
            temp.Add("Mes-Embargo", dc_date_embargo_month);
            temp.Add("Ano-Embargo", dc_date_embargo_year);
            temp.Add("Licensa", dc_rights_uri);

            return temp;
        }
    }
}