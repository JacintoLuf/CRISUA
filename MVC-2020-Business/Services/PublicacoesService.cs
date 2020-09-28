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
using MVC_2020_Business.Models.Authenticus;
using MVC_2020_Database;
using MVC_2020_Database.DataModels;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Xml.Serialization;

namespace MVC_2020_Business.Services
{
    public class PublicacoesService : BaseService
    {

        private const string URL = "https://ptcrisync-api.herokuapp.com/rest/import/";
        private const string importDifURL = "https://ptcrisync-api.herokuapp.com/rest/import/works/";
        private const string urlParameter = "0000-0002-5228-0329";
        private const string urlParameter2 = "0000-0002-0417-9402";
        private const string urlParameter5 = "0000-0002-4356-4522"; //Vieira
        private const string urlParameter3 = "0000-0002-7128-2805"; //Filipe Trancho
        private const string urlParameter4 = "0000-0002-3488-6570"; //Renato
        private const string urlParameter6 = "0000-0003-4186-7332"; //Rosalino
        private const string iupi = "66c74f1f-8c45-4f43-9a85-be4975eecc09"; // vieira
        private const string iupi2 = "83b90544-a39d-4073-81cb-0ad094c1ec71"; // rosalino

        //public static IEnumerable<Product> GetProducts()
        //{
        //    var client = new RestClient("https://ria.ua.pt/");
        //    var request = new RestRequest("rest/items/find-by-metadata-field", Method.POST);
        //    request.AddJsonBody(new Person("ua.dados.iupi", "66c74f1f-8c45-4f43-9a85-be4975eecc09", "pt_PT")); //50ca9826-f8c9-46aa-a9b7-10d03dc63f39
        //    var queryResult = client.Execute(request);
        //    if (!queryResult.IsSuccessful || queryResult.Content == null) { return new List<Product>(); }
        //    return JsonConvert.DeserializeObject<List<Product>>(queryResult.Content);
        //}

        public static List<Product> GetProducts(MyDbContext _db, string IUPI, string fullName)
        {
            //Save<jobs>(new jobs() { job_desc = "Renato2 Artigo", min_lvl = 12, max_lvl = 22 });
            /*var per = _db.Person
                //.Where(b => b.GenderId > 2)
                //.OrderBy(b => b.GenderId)
                .ToList();*/
            //IUPI = iupi;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://ria.ua.pt/RESTRia-1.0/publications/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "Y3Jpczo4ZjNqRXhnUXdoUmNmc2x5NkZyLg==");

            var queryResult = client.GetAsync(IUPI).Result;//"7f8b8645-515e-48bf-bf58-613ab7a6244d" dg

            if (!queryResult.IsSuccessStatusCode || queryResult.Content == null) { return new List<Product>(); }

            var desPub = JsonConvert.DeserializeObject<List<Product>>(queryResult.Content.ReadAsStringAsync().Result);
            DatabaseServices.insertPublicationsRIA(_db, desPub, fullName);// ------ INSERIR PUBLICAÇÕES DO RIA NA BD




            //var dois = _db.Publication_Identifier.Select(x => x.Value).ToList();
            //var pubs = new List<Publication>();
            //var pubsId = new List<Publication_Identifier>();
            //var contador = 2;
            //foreach (var pub in desPub)
            //{
            //    if ( !dois.Contains(pub.Doi) && !String.IsNullOrWhiteSpace(pub.Doi))
            //    {
            //        pubs.Add(new Publication() { LanguageId = 1, Date = DateTime.Now, Source = "RIA", Synced = true }); //parametros mal dados
            //        //pubsId.Add(new Publication_Identifier() { PublicationId=pubs.First().PublicationId, IdentifierId=1, StartDate= DateTime.Now, EndDate= DateTime.Now, Value=pub.Doi });

            //    }
            //}
            //_db.Set<Publication>().AddRange(pubs);
            ////_db.Set<Publication_Identifier>().AddRange(pubsId);
            //_db.SaveChanges();
            return desPub;
        }

        public static List<Product> GetProducts2(MyDbContext _db, string IUPI)
        {
            IUPI = iupi;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://ria.ua.pt/RESTRia-1.0/publications/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "Y3Jpczo4ZjNqRXhnUXdoUmNmc2x5NkZyLg==");

            var queryResult = client.GetAsync(IUPI).Result;//"7f8b8645-515e-48bf-bf58-613ab7a6244d" dg

            if (!queryResult.IsSuccessStatusCode || queryResult.Content == null) { return new List<Product>(); }

            var desPub = JsonConvert.DeserializeObject<List<Product>>(queryResult.Content.ReadAsStringAsync().Result);
            return desPub;
        }


        //public static Models.Orcid.Works GetWorksFromXml() //Buscar ao Orcid
        //{
        //    HttpClient client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("Accept", "application/json");
        //    var request = client.GetStringAsync("https://pub.orcid.org/v2.1/0000-0002-3488-6570/works");
        //    return JsonConvert.DeserializeObject<Models.Orcid.Works>(request.Result);
        //}

        public static List<Work> GetWorks() //Buscar ao PTCRIS
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(urlParameter5).Result;

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

        public static List<Work> GetDifWorks(MyDbContext _db, string full_name,  List<Work> locals, string iupi, string orcidID)
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
            HttpResponseMessage response2 = client2.PostAsync(orcidID, stringContent).Result;
            if (response2.IsSuccessStatusCode)
            {
                var resultado = response2.Content.ReadAsStringAsync().Result;
                var ls = JsonConvert.DeserializeObject<List<Work>>(resultado);
                ls = RemoveDuplicates(ls, full_name.Split(" ").Last());
                DatabaseServices.insertPublicationsPTCRIS(_db, full_name, ls, orcidID, iupi ); //-----   INSERIR PUBLICAÇÕES VINDAS DO PTCRIS NA BD
                return ls;
            }
            else
            {
                return new List<Work>();
            }
        }

        public static List<Work> GetDifWorks2(MyDbContext _db, List<Work> locals)
        {
            HttpClient client2 = new HttpClient();
            client2.BaseAddress = new Uri(importDifURL);

            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(locals);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response2 = client2.PostAsync(urlParameter5, stringContent).Result;
            if (response2.IsSuccessStatusCode)
            {
                var resultado = response2.Content.ReadAsStringAsync().Result;
                var ls = JsonConvert.DeserializeObject<List<Work>>(resultado);
                return ls;
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

        private static List<Work> RemoveDuplicates(List<Work> works, string nome) //falta meter o nome de utilizador como argumento
        {
            List<Work> juntos = works.ToList(); //lista para guardar as publicações que se fez o merge
            var titulosDuplicados = juntos.GroupBy(x => x.title.title.ToLower()).Where(x => x.Count() > 1).Select(x => x.Key).ToList(); //lista de titulos duplicados
            juntos.Clear();
            titulosDuplicados.ForEach(x => Console.WriteLine(x));
            //Console.WriteLine("duplicados count: " + titulosDuplicados.Count + "\n||||||||||||||||||||");
            foreach (string d in titulosDuplicados)
            {
                List<Work> listToMerge = works.FindAll(x => x.title.title.ToLower().Equals(d)); //buscar as publicações repetidas
                listToMerge.Sort((x, y) => y.externalIds.externalId.Count.CompareTo(x.externalIds.externalId.Count)); //ordena por ordem descendente do numero de external ids 
                //Console.WriteLine("Duplicados: ");
                //listToMerge.ForEach(x => {
                //    Console.WriteLine("\t" + x.title.title + "||" + x.source.sourceName.content);
                //    List<Models.ExternalId> eids = x.externalIds.externalId;
                //    eids.ForEach(ei => {
                //        Console.WriteLine("\t\t" + ei.externalIdType);
                //        Console.WriteLine("\t\t" + ei.externalIdValue);
                //    });
                //});
                works = works.Except(listToMerge).ToList(); //tira do works as publicações repetidas
                //Console.WriteLine("Works count after remove duplicates: " + works.Count);

                Work toStay = listToMerge.Find(x => x.source.sourceName.content.ToLower().Contains("crossref")
                                                    || x.source.sourceName.content.ToLower().Contains("scopus")
                                                    || !x.source.sourceName.content.ToLower().Contains(nome)
                                                    || !x.source.sourceName.content.ToLower().Contains("datacite")); //escolhe a publicação que vem do crossref, scopus ou o que não vem do autor e datacite
                if (toStay == null) toStay = listToMerge.Find(x => x.source.sourceName.content.ToLower().Contains(nome)); //se não escolheu nenhuma escolhe a do autor

                listToMerge.Remove(toStay);// remove a publicação escolhida da lista das publicações repetidas
                while (listToMerge.Count != 0) //ve os duplicados todos
                {
                    Work temp;
                    if (listToMerge.Count > 1)
                    {
                        temp = listToMerge.Find(x => !x.source.sourceName.content.ToLower().Contains(nome)
                                                    || x.source.sourceName.content.ToLower().Contains("scopus")); //escolhe a publicação que não vem do autor
                    }
                    else
                    {
                        temp = listToMerge.First();
                    }
                    listToMerge.Remove(temp);
                    if (toStay.visibility == null) toStay.visibility = temp.visibility;
                    if (toStay.displayIndex == null) toStay.displayIndex = temp.displayIndex;
                    if (toStay.title.title == null) toStay.title.title = temp.title.title;
                    if (toStay.title.subtitle == null) toStay.title.subtitle = temp.title.subtitle;
                    if (toStay.title.translatedTitle == null) toStay.title.translatedTitle = temp.title.translatedTitle;
                    if (toStay.journalTitle == null || (toStay.journalTitle.content.Equals("Unpublished") && temp.journalTitle != null)) toStay.journalTitle = temp.journalTitle;
                    if (toStay.shortDescription == null) toStay.shortDescription = temp.shortDescription;
                    if (toStay.citation == null) toStay.citation = temp.citation;
                    if (toStay.type == null || (toStay.type.Equals("OTHER") && temp.type != null)) toStay.type = temp.type;
                    if (toStay.publicationDate == null) toStay.publicationDate = temp.publicationDate;
                    if (toStay.externalIds == null) toStay.externalIds = temp.externalIds;
                    else
                    {
                        if (temp.externalIds != null)
                        {
                            temp.externalIds.externalId.ForEach(eid =>
                            {
                                if (!toStay.externalIds.externalId.Exists(ei => ei.externalIdType.Equals(eid.externalIdType)))
                                    toStay.externalIds.externalId.Add(eid);
                            });
                        }
                    }
                    if (toStay.url == null) toStay.url = temp.url;
                    if (toStay.contributors == null || toStay.contributors.contributor.Count == 0) toStay.contributors = temp.contributors;
                    if (toStay.languageCode == null) toStay.languageCode = temp.languageCode;
                    if (toStay.country == null) toStay.country = temp.country;
                }

                juntos.Add(toStay); //adicionar a publicação em que se fez o merge
            }
            //juntos.ForEach(w =>
            //{
            //    Console.WriteLine(w.title.title);
            //    Console.WriteLine(w.source.sourceName.content);
            //    if (w.country != null) Console.WriteLine("Country: " + w.country.value);
            //    if (w.languageCode != null) Console.WriteLine("Language: " + w.languageCode);
            //    if (w.journalTitle != null) Console.WriteLine("journal: " + w.journalTitle.content);
            //    Console.WriteLine("Type: " + w.type);
            //    Console.WriteLine("ExternalIds:");
            //    w.externalIds.externalId.ForEach(eid =>
            //    {
            //        Console.WriteLine("\tTipo: " + eid.externalIdType);
            //        Console.WriteLine("\tValor: " + eid.externalIdValue);
            //        Console.WriteLine("\t---------");
            //    });
            //    if (w.citation != null) Console.WriteLine("citation: " + w.citation.citationValue);
            //    Console.WriteLine("------------------------------------------------------------");

            //});
            works.AddRange(juntos); //adicionar a lista de publicações em que se fez o merge às outras
            //Console.WriteLine("Final works count: " + works.Count);
            return works;
        }

        public static T DeserializeXML<T>(string xmlContent)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent));
            return (T)serializer.Deserialize(memStream);
        }

        public static async Task<page> getResearchersAuthenticus(string institution)
        {
            HttpClient client = new HttpClient();
            // Institution needs to be validated at some point
            client.BaseAddress = new Uri("https://www.authenticus.pt/api/v2.0/institutions/" + institution + "/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "gnrqOxEs7Aa5o6K47P6xmEQEs5URIC6avQ0N");

            HttpResponseMessage queryResult = client.GetAsync("researchers").Result;

            if (!queryResult.IsSuccessStatusCode)
            {
                Console.WriteLine(queryResult.StatusCode);
                return null;
            }
            else
            {
                string resultXml = await queryResult.Content.ReadAsStringAsync();
                page resultObject = DeserializeXML<page>(resultXml);

                return resultObject;
            }
        }

        public static async Task<page> getAuthPubsAuthenticus(string authID)
        {
            HttpClient client = new HttpClient();
            // authID needs to be validated at some point
            client.BaseAddress = new Uri("https://www.authenticus.pt/api/v2.0/researchers/" + authID + "/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "gnrqOxEs7Aa5o6K47P6xmEQEs5URIC6avQ0N");

            HttpResponseMessage queryResult = client.GetAsync("publications").Result;

            if (!queryResult.IsSuccessStatusCode)
            {
                Console.WriteLine(queryResult.StatusCode);
                return null;
            }
            else
            {
                string resultXml = await queryResult.Content.ReadAsStringAsync();
                page resultObject = DeserializeXML<page>(resultXml);

                return resultObject;
            }
        }

        public static async Task<publication> getPubDataAuthenticus(string pubID)
        {
            HttpClient client = new HttpClient();
            // Institution needs to be validated at some point
            client.BaseAddress = new Uri("https://www.authenticus.pt/api/v2.0/publications/" + pubID);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "gnrqOxEs7Aa5o6K47P6xmEQEs5URIC6avQ0N");

            HttpResponseMessage queryResult = client.GetAsync("").Result;

            if (!queryResult.IsSuccessStatusCode)
            {
                Console.WriteLine(queryResult.StatusCode);
                return null;
            }
            else
            {
                string resultXml = await queryResult.Content.ReadAsStringAsync();
                publication resultObject = DeserializeXML<publication>(resultXml);

                return resultObject;
            }
        }

        public static void printPublicationAuthenticus(publication pub)
        {
            Console.WriteLine("type :" + pub.type[0].Value);
            Console.WriteLine("title :" + pub.title);
            foreach (publicationAuthor item in pub.Items)
            {
                Console.WriteLine("author :" + item.name);
            }
            Console.WriteLine("year: " + pub.year);
            Console.WriteLine("abstract :" + pub.@abstract);
            Console.WriteLine("doi :" + pub.doi);
            Console.WriteLine("source-title :" + pub.sourcetitle);
            Console.WriteLine("issn :" + pub.issn);
            Console.WriteLine("volume :" + pub.volume);
            return;
        }
        public static async Task<researcher> getResearcherInfoAuthenticus(string authID)
        {
            HttpClient client = new HttpClient();
            // authID needs to be validated at some point
            client.BaseAddress = new Uri("https://www.authenticus.pt/api/v2.0/researchers/" + authID);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "gnrqOxEs7Aa5o6K47P6xmEQEs5URIC6avQ0N");

            HttpResponseMessage queryResult = client.GetAsync("").Result;

            if (!queryResult.IsSuccessStatusCode)
            {
                Console.WriteLine(queryResult.StatusCode);
                return null;
            }
            else
            {
                string resultXml = await queryResult.Content.ReadAsStringAsync();
                researcher resultObject = DeserializeXML<researcher>(resultXml);
                Console.WriteLine("Researcher: " + authID + " has orcid: " + resultObject.externalid[0].Value);
                return resultObject;
            }
        }


        public static List<publication> allResearchersPubsAuthenticus(string authID)
        {
            // authID needs to be validated at some point
            page authPubs = getAuthPubsAuthenticus(authID).Result;
            List<publication> allPubs = new List<publication>();

            foreach (filteredResource item in authPubs.items)
                allPubs.Add(getPubDataAuthenticus(item.id).Result);

            return allPubs;
        }
    }
}
