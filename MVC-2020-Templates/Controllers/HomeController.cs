using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_2020_Business.Models;
using MVC_2020_Business.Services;
using MVC_2020_Database.DataModels;
using MVC_2020_Template.Helpers;
using MVC_2020_Template.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack;
using ServiceStack.OrmLite.Converters;


namespace MVC_2020_Template.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static MyDbContext _db;// acesso db
        private static Dictionary<string, List<string>> _ficheiros = new Dictionary<string, List<string>>();
        private IHostingEnvironment _hostingEnvironment;
        private static Session _session;

        public HomeController(ILogger<HomeController> logger, MyDbContext db, IHostingEnvironment hosting)// acesso db
        {
            _logger = logger;
            _db = db;// acesso db
            _hostingEnvironment = hosting;
        }

        public static MyDbContext DB()
        {
            return _db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Documentation()
        {
            return File("~/Doxygen/html/index.html", "text/html");
        }

        public IActionResult Admin()
        {
            {
                var tempOrgUnit = _db.OrgUnit.ToList();
                var listOrgUnit = new List<UnidadeInvestigacao>();
                foreach (var temp in tempOrgUnit)
                {

                    listOrgUnit.Add(DatabaseServices.retrieveInfoUI(_db, temp.OrgUnitId));

                }
                ViewBag.OrgUnits = listOrgUnit.ToList();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Admin(string adicionar_bt, string cancelar_bt, string nome, string acronimo, DateTime data_ini, DateTime data_fim,
                                    string uri, double fraction, string value, int orgUnitId2, int addressId, int langId,
                                    string activityText, string keywords)
        {
            if (cancelar_bt == "cancel")
            {
                return RedirectToAction("Admin", "Home");
            }

            //Adiciona nova UI
            if (adicionar_bt == "add")
            {
                Console.WriteLine("New UI");
                DatabaseServices.insertOrgUnit(DB(), acronimo, uri, data_ini, data_fim, fraction,
                                value, orgUnitId2, addressId, langId, activityText, keywords, nome);

            }

            return RedirectToAction("Admin", "Home");
        }

        public IActionResult UI_Details(String obj)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(obj);
            return View();
        }

        [HttpPost]
        public IActionResult UI_Details(string org,int id, string guardar_bt, string cancelar_bt, string nome, string acronimo, DateTime data_ini, DateTime data_fim,
                                    string uri, double fraction, string value, int orgUnitId2, int addressId, int langId,
                                    string activityText, string keywords)
        {
            if (cancelar_bt == "cancel")
            {
                return RedirectToAction("UI_Details", "Home", new { obj = org } );
            }

            //Guarda dados editados na BD
            if (guardar_bt == "guardar")
            {
                Console.WriteLine("Edited UI");
                DatabaseServices.updateOrgUnit(DB(),id, nome, acronimo,  data_ini, data_fim, uri, fraction,
                                value, orgUnitId2, addressId, langId, activityText, keywords);

                var orgunitUpdate = DatabaseServices.retrieveInfoUI(_db,id);

                return RedirectToAction("UI_Details", "Home", new { obj = JsonConvert.SerializeObject(orgunitUpdate) });

            }

            return RedirectToAction("UI_Details", "Home", new { obj = org});
        }

        public IActionResult UI_Details_Helper(String obj)
        {
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(obj);
            return Json(Url.Action("UI_Details", "Home", new { obj = obj }));
        }
        public IActionResult DeleteUI_Helper(string org)
        {
            //chamar funcao delete da ui
            var id = Int32.Parse(@Newtonsoft.Json.JsonConvert.DeserializeObject(org).ToString());
            DatabaseServices.deleteOrgUnit(_db, id);
            Console.WriteLine(org);
            return View("Close");

        }

        public IActionResult Close()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Perfil()
        {
            _session = new Session(HttpContext.Session);
            ////Estou a passar o IUPI do Vieira pãra testar
            var aux = MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, _session.IUPI.ToString());  //IR buscar o Orcid ID à BD

            if (aux != null)
            {
                ViewBag.OrcidID = aux;
            }
            else
            {
                ViewBag.OrcidID = null;
            }

            //ViewBag.OrcidID = null;
            return View();
        }
        [HttpPost]
        public IActionResult Perfil(String OrcidID)
        {
            
            DatabaseServices.insertLoginPerson(_db, _session.FullName.ToString(), OrcidID, _session.IUPI.ToString());

            //MVC_2020_Business.Services.DatabaseServices.setOrcid(_db, _session.IUPI.ToString(), OrcidID); //meter o OrcidID na BD
            //ViewBag.OrcidID = "0000-0002-3488-6570";
            ViewBag.OrcidID = MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, _session.IUPI.ToString());
            return View();
        }
        public IActionResult MyPublications()
        {
            
            ViewBag.pubsInBD = MVC_2020_Business.Services.DatabaseServices.selectAllPubsInBD(_db, "Publication", "State", MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, _session.IUPI.ToString()), _session.IUPI.ToString());
           // ViewBag.pubsInBD = MVC_2020_Business.Services.DatabaseServices.selectToRIA(_db, MVC_2020_Business.Services.DatabaseServices.selectAllPubsInBD(_db, "Publication", "State", "0000-0002-4356-4522"), _session.IUPI.ToString());
            return View();
        }

        [HttpGet]
        public IActionResult Publicacoes()
        {
            
            //ViewBag.PublicacoesRIA = PublicacoesService.GetProducts(_db, _session.IUPI.ToString());
            //ViewBag.PublicacoesOrcid = PublicacoesService.GetWorksFromXml();
            ViewBag.worksInBD = MVC_2020_Business.Services.DatabaseServices.selectToRIA(_db, MVC_2020_Business.Services.DatabaseServices.select(_db, "Publication", "State", "1", MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, _session.IUPI.ToString())), _session.IUPI.ToString());
            ViewBag.OrcidID = MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, _session.IUPI.ToString());
            //ViewBag.PublicacoesPTCris = PublicacoesService.GetDifWorks(_db,
            //                            PublicacoesService.ConvertProductToWork(
            //                            PublicacoesService.GetProducts(_db, _session.IUPI.ToString())));
            ViewBag.import = "false";
            return View();
        }

        [HttpPost]
        public IActionResult Publicacoes(string import, string ORCID_source, string Authenticus_source)
        {

            if (ORCID_source == "ORCID" /*&& import == "True"*/) // Fonte de dados - ORCID
            {
                ViewBag.dataSource = "ORCID";

                //ViewBag.PublicacoesRIA = PublicacoesService.GetProducts(_db, Session.IUPI.ToString());
                //ViewBag.PublicacoesOrcid = PublicacoesService.GetWorksFromXml();
                ViewBag.PublicacoesPTCris = PublicacoesService.GetDifWorks(_db, _session.FullName,
                                            PublicacoesService.ConvertProductToWork(
                                            PublicacoesService.GetProducts(_db, _session.IUPI.ToString(), _session.FullName)), _session.IUPI.ToString(), MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, _session.IUPI.ToString()));
                ViewBag.worksInBD = MVC_2020_Business.Services.DatabaseServices.selectToRIA(_db, MVC_2020_Business.Services.DatabaseServices.select(_db, "Publication", "State", "1", MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, _session.IUPI.ToString())), _session.IUPI.ToString());


                ViewBag.import = import;
            }
            else if (Authenticus_source == "Authenticus") // Fonte de dados - Authenticus
            {
                ViewBag.dataSource = "Authenticus";
            }

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
            var listaPrint = ((MVC_2020_Business.Services.DatabaseServices.select(_db, "Publication", "State", "2", MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, _session.IUPI.ToString()))));
            listaPrint.AddRange(MVC_2020_Business.Services.DatabaseServices.select(_db, "Publication", "State", "3", MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, _session.IUPI.ToString())));
            listaPrint.AddRange(MVC_2020_Business.Services.DatabaseServices.select(_db, "Publication", "State", "4", MVC_2020_Business.Services.DatabaseServices.getOrcid(_db, _session.IUPI.ToString())));
            ViewBag.PubSaved = MVC_2020_Business.Services.DatabaseServices.selectToRIA(_db, listaPrint, _session.IUPI.ToString());
            //ViewBag.PublicacoesPTCris2 = PublicacoesService.GetDifWorks2(_db,
            //                            PublicacoesService.ConvertProductToWork(
            //                            PublicacoesService.GetProducts2(_db, _session.IUPI.ToString())));
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
                                                    string dc_type, string dc_description_version, string dc_language_iso, IFormCollection fc, string submit_dc_contributor_author_add,
                                                    string pubID)
        {
            
            Hashtable work = @Newtonsoft.Json.JsonConvert.DeserializeObject<Hashtable>(works);
            if (submit_dc_contributor_author_add == "Add More" && dc_contributor_author != null)
            {

                JArray aut = (JArray)work["Autores"];
                aut.Add(dc_contributor_author);
                work.Remove("Autores");
                work.Add("Autores", aut);
                string works2 = JsonConvert.SerializeObject(work);
                return RedirectToAction("PublicationMetaData1", "Home", new { works = works2 });

            }
            Hashtable dados = publicationMeta1ToHash(fc["dc_contributor_author_1"].ToList(), dc_title, dc_title_alternative, degois_publication_title /*revista*/, dc_date_issued_day,
                                                     dc_date_issued_month, dc_date_issued_year, degois_publication_volume, degois_publication_issue,
                                                     degois_publication_firstPage, degois_publication_lastPage, dc_publisher, dc_identifier_issn,
                                                     dc_identifier_essn, dc_identifier_doi, dc_peerreviewed, dc_relation_publisherversion,
                                                     dc_type, dc_description_version, dc_language_iso, pubID);

            DatabaseServices.updateAsTable1(_db, dados);
            //var teste = fc["dc_contributor_author_1"].ToList();
            //Console.WriteLine("TESTE: ");
            //teste.ForEach(x => Console.WriteLine("\t"+x));


            if (submit_next == "Next")
            {
                //Console.WriteLine("Titulo: " + dc_title);
                //Console.WriteLine("Autores: " + dc_contributor_author);

                work.Remove("Autores");
                work.Add("Autores", fc["dc_contributor_author_1"].ToList());
                //string works2 = JsonConvert.SerializeObject(work);
                MVC_2020_Business.Services.DatabaseServices.updateState(_db, Int32.Parse(pubID), 3);
                Hashtable info = DatabaseServices.retrieveAllInfo(_db, work["titulo"].ToString(), _session.IUPI.ToString());
                info["Autores"] = work["Autores"];
                string works2 = JsonConvert.SerializeObject(info);
                return RedirectToAction("PublicationMetaData2", "Home", new { works = works2 });
            }
            if (submit_cancel == "Cancel/Save")
            {
                Hashtable info = DatabaseServices.retrieveAllInfo(_db, work["titulo"].ToString(), _session.IUPI.ToString());
                string works2 = JsonConvert.SerializeObject(info);
                MVC_2020_Business.Services.DatabaseServices.updateState(_db, Int32.Parse(pubID), 3);
                return RedirectToAction("PublicacoesSalvas", "Home");
            }


            return RedirectToAction("PublicationMetaData1", "Home", new { works = works });
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
                                                    string dc_description_sponsorship /*patrocinadores*/, string dc_rights /*acesso*/,string dc_date_embargo_day, 
                                                    string dc_date_embargo_month, string dc_date_embargo_year, string dc_rights_uri /*licensa*/, string pubID)
        {
            
            Hashtable dados = publicationMeta2ToHash(dc_subject_1 /*palavra chave*/, dc_description_abstract_1 /*resumo*/, dc_relation_authority, /*prof financiado*/
                                                     dc_description_sponsorship /*patrocinadores*/, dc_rights /*acesso*/, dc_date_embargo_day, dc_date_embargo_month, 
                                                     dc_date_embargo_year, dc_rights_uri /*licensa*/, pubID);

            DatabaseServices.updateAsTable2(_db, dados);
            Hashtable work = @Newtonsoft.Json.JsonConvert.DeserializeObject<Hashtable>(works);
            Hashtable info = DatabaseServices.retrieveAllInfo(_db, work["titulo"].ToString(), _session.IUPI.ToString());
            string works2 = JsonConvert.SerializeObject(info);

            if (submit_jump_2_1 == "Describe" || submit_prev == "Previous")
                return RedirectToAction("PublicationMetaData1", "Home", new { works = works2 });
            if (submit_next == "Next")
            {
                return RedirectToAction("PublicationMetaData3", "Home", new { works = works2 });
            }
            if (submit_cancel == "Cancel/Save")
                return RedirectToAction("PublicacoesSalvas", "Home");

            return RedirectToAction("PublicationMetaData2", "Home", new { works = works2 });
        }

        public IActionResult PublicationMetaData2_Helper(String works, string dc_title)
        {
            Console.WriteLine("Titulo: " + dc_title);
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            return Json(Url.Action("PublicationMetaData2", "Home", new { works = works }));
        }

        public IActionResult PublicationMetaData3(String works /*List<String> files*/)
        {
            Hashtable work = @Newtonsoft.Json.JsonConvert.DeserializeObject<Hashtable>(works);
            ViewBag.dados = @Newtonsoft.Json.JsonConvert.DeserializeObject(works);
            if (_ficheiros.ContainsKey(work["ID"].ToString()))
                ViewBag.files = _ficheiros[work["ID"].ToString()];
            else
                ViewBag.files = new List<string>();
            return View();
        }

        [HttpPost]
        public IActionResult PublicationMetaData3(string works, string submit_next, string submit_cancel, string submit_prev, string submit_jump_2_1, string submit_jump_2_2,
                                                    List<IFormFile> files, string submit_more, IFormCollection fc, string id)
        {
            var list_files = fc["file"].ToList();
            List<string> temp = new List<string>();
            if (_ficheiros.ContainsKey(id))
            {
                list_files.ForEach(x =>
                {
                    if (_ficheiros[id].Contains(x))
                        temp.Add(x);
                });
                _ficheiros[id] = temp;
            }

            var filePaths = new List<String>();
            if (submit_more == "Add Another File")
            {
                var size = files.Sum(f => f.Length);
                string diretorio = Path.Combine(_hostingEnvironment.WebRootPath + "\\Ficheiros");

                foreach (var formfile in files)
                {
                    if (formfile.Length > 0)
                    {

                        //if (!Directory.Exists(diretorio))
                        //    Directory.CreateDirectory(diretorio);
                        //var filePath = Path.Combine(diretorio, formfile.FileName);
                        //if (System.IO.File.Exists(filePath))
                        //    filePath = Path.Combine(diretorio, "a" + formfile.FileName);
                        var filePath = formfile.FileName + ";" + formfile.Length + ";" + formfile.ContentType;
                        filePaths.Add(filePath);
                        ////using (var stream = new FileStream(filePath, FileMode.Create))
                        ////{
                        ////    await formfile.CopyToAsync(stream);
                        ////}
                    }
                }
                if (_ficheiros.ContainsKey(id))
                    _ficheiros[id].AddRange(filePaths);
                else
                    _ficheiros.Add(id, filePaths);
                return RedirectToAction("PublicationMetaData3", "Home", new { works = works/*, files = filePaths*/ });

            }
            if (submit_jump_2_1 == "Describe")
                return RedirectToAction("PublicationMetaData1", "Home", new { works = works/*, files = filePaths*/ });
            if (submit_jump_2_2 == "Describe" || submit_prev == "Previous")
                return RedirectToAction("PublicationMetaData2", "Home", new { works = works/*, files = filePaths*/ });
            if (submit_next == "Next")
            {
                return RedirectToAction("PublicationSubmission", "Home", new { works = works/*, files = filePaths*/ });
            }
            if (submit_cancel == "Cancel/Save")
                return RedirectToAction("PublicacoesSalvas", "Home");

            return RedirectToAction("PublicationMetaData3", "Home", new { works = works/*, files = filePaths*/ });
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

            return RedirectToAction("PublicationSubmission", "Home", new { works = works/*, files = filePaths*/ });
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
        public IActionResult dSPaceLicense(string works, string submit_reject, string submit_grant, string submit_jump_2_1, 
                                            string submit_jump_2_2, string submit_jump_3_1, string submit_jump_4_1, string pubID)
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
                MVC_2020_Business.Services.DatabaseServices.updateState(_db, Int32.Parse(pubID), 4);
                return RedirectToAction("PublicacoesSalvas", "Home");
            }

            return RedirectToAction("dSpaceLicense", "Home", new { works = works });
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

        public IActionResult DataSource()
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

        public static Hashtable publicationMeta1ToHash(List<string> dc_contributor_author, string dc_title,
                                                    string dc_title_alternative, string degois_publication_title /*revista*/, string dc_date_issued_day,
                                                    string dc_date_issued_month, string dc_date_issued_year, string degois_publication_volume, string degois_publication_issue,
                                                    string degois_publication_firstPage, string degois_publication_lastPage, string dc_publisher, string dc_identifier_issn,
                                                    string dc_identifier_essn, string dc_identifier_doi, string dc_peerreviewed, string dc_relation_publisherversion,
                                                    string dc_type, string dc_description_version, string dc_language_iso, string pubID)
        {
            Hashtable temp = new Hashtable();
            temp.Add("titulo", dc_title);
            temp.Add("Autores", dc_contributor_author);
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
            temp.Add("ID", pubID);

            //Console.WriteLine("HASHTABLE AUTORES: ");
            //foreach (var d in (List<string>)temp["Autores"])
            //{
            //    Console.WriteLine(d);
            //}
                

            return temp;
        }

        public static Hashtable publicationMeta2ToHash(string dc_subject_1 /*palavra chave*/, string dc_description_abstract_1 /*resumo*/, string dc_relation_authority, /*prof financiado*/
                                                        string dc_description_sponsorship /*patrocinadores*/, string dc_rights /*acesso*/, string dc_date_embargo_day, 
                                                        string dc_date_embargo_month, string dc_date_embargo_year, string dc_rights_uri /*licensa*/, string pubID)
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
            temp.Add("ID", pubID);

            return temp;
        }
    }
}