using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MVC_2020_Business.Models;
using MVC_2020_Business.Models.Orcid;
using MVC_2020_Database;
using MVC_2020_Database.DataModels;
using Newtonsoft.Json;
using RestSharp;

namespace MVC_2020_Business.Services
{
    public class PublicacoesService : BaseService
    {

        private const string URL = "https://ptcrisync-api.herokuapp.com/rest/import/";
        private const string importDifURL = "https://ptcrisync-api.herokuapp.com/rest/import/works/";
        private const string urlParameter = "0000-0002-5228-0329";
        private const string urlParameter2 = "0000-0002-0417-9402";
        private const string urlParameter3 = "0000-0002-4356-4522"; //vieira
        private const string urlParameter4 = "0000-0002-3488-6570"; //Renato

        //public static IEnumerable<Product> GetProducts()
        //{
        //    var client = new RestClient("https://ria.ua.pt/");
        //    var request = new RestRequest("rest/items/find-by-metadata-field", Method.POST);
        //    request.AddJsonBody(new Person("ua.dados.iupi", "66c74f1f-8c45-4f43-9a85-be4975eecc09", "pt_PT")); //50ca9826-f8c9-46aa-a9b7-10d03dc63f39
        //    var queryResult = client.Execute(request);
        //    if (!queryResult.IsSuccessful || queryResult.Content == null) { return new List<Product>(); }
        //    return JsonConvert.DeserializeObject<List<Product>>(queryResult.Content);
        //}

        public static List<Product> GetProducts(MyDbContext _db, string IUPI)
        {
            //Save<jobs>(new jobs() { job_desc = "Renato2 Artigo", min_lvl = 12, max_lvl = 22 });
            var per = _db.Person
                //.Where(b => b.GenderId > 2)
                //.OrderBy(b => b.GenderId)
                .ToList();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://ria.ua.pt/RESTRia-1.0/publications/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "Y3Jpczo4ZjNqRXhnUXdoUmNmc2x5NkZyLg==");

            var queryResult = client.GetAsync(IUPI).Result;//"7f8b8645-515e-48bf-bf58-613ab7a6244d" dg

            if (!queryResult.IsSuccessStatusCode || queryResult.Content == null) { return new List<Product>(); }

            var desPub = JsonConvert.DeserializeObject<List<Product>>(queryResult.Content.ReadAsStringAsync().Result);

            var dois = _db.Publication_Identifier.Select(x => x.Value).ToList();
            var pubs = new List<Publication>();
            var pubsId = new List<Publication_Identifier>();
            var contador = 2;
            foreach (var pub in desPub)
            {
                if ( !dois.Contains(pub.Doi) && !String.IsNullOrWhiteSpace(pub.Doi))
                {
                    pubs.Add(new Publication() { LanguageId = 1, Date = DateTime.Now, Source = "RIA", Synced = true }); //parametros mal dados
                    //pubsId.Add(new Publication_Identifier() { PublicationId=pubs.First().PublicationId, IdentifierId=1, StartDate= DateTime.Now, EndDate= DateTime.Now, Value=pub.Doi });
                    
                }
            }
            _db.Set<Publication>().AddRange(pubs);
            //_db.Set<Publication_Identifier>().AddRange(pubsId);
            _db.SaveChanges();
            return desPub;
        }


        public static Models.Orcid.Works GetWorksFromXml() //Buscar ao Orcid
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var request = client.GetStringAsync("https://pub.orcid.org/v2.1/0000-0002-3488-6570/works");
            return JsonConvert.DeserializeObject<Models.Orcid.Works>(request.Result);
        }

        public static List<Work> GetWorks() //Buscar ao PTCRIS
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(urlParameter4).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Work>>(res);
            }
            else
            {
                return new List<Work>();
            }
        }

        public static List<Work> GetDifWorks(List<Work> locals)
        {
            //using (var db = new PersingContext())
            //{
            //    var blogs = db.Persons
            //        .Where(b => b.GenderId > 2)
            //        .OrderBy(b => b.GenderId)
            //        .ToList();
            //}
            HttpClient client2 = new HttpClient();
            client2.BaseAddress = new Uri(importDifURL);

            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(locals);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response2 = client2.PostAsync(urlParameter4, stringContent).Result;
            if (response2.IsSuccessStatusCode)
            {
                var resultado = response2.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Work>>(resultado);
            }
            else
            {
                return new List<Work>();
            }
        }


        public static List<Work> ConvertProductToWork(List<Product> ria)
        {
            List<Work> works = new List<Work>();
            string handleBaseUrl = "http://hdl.handle.net/";
            foreach (Product p in ria)
            {
                Work w = new Work();
                Title title = new Title(p.Title);
                w.title = title;
                //w.url = new Url(handleBaseUrl + p.Handle);

                Models.ExternalIds externalIds = new Models.ExternalIds(new List<Models.ExternalId>());
                Models.ExternalId eid = new Models.ExternalId("handle", handleBaseUrl + p.Handle, ExternalIdRelationship.SELF.ToString());
                externalIds.externalId.Add(eid);

                if (p.Doi != null)
                {
                    Models.ExternalId externalId = new Models.ExternalId("doi", p.Doi, ExternalIdRelationship.SELF.ToString());
                    externalIds.externalId.Add(externalId);
                }
                w.externalIds = externalIds;

                works.Add(w);
            }
            return works;
        }
    }
}
