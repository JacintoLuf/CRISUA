//using Newtonsoft.Json.Linq;
//using System;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
//using System.Collections.Generic;
//using Nancy.Json;
//using System.Text;
//using RestSharp;
//using MVC_2020_Business.Models;
//using System.Collections;
//using System.Linq;

//namespace ConsoleApp2
//{
//    class Program
//    {
//        private const string URL = "https://ptcrisync-api.herokuapp.com/rest/import/";
//        private const string importDifURL = "https://ptcrisync-api.herokuapp.com/rest/import/works/";
//        private const string urlParameter = "0000-0002-5228-0329";
//        private const string urlParameter2 = "0000-0002-0417-9402";
//        private const string urlParameter3 = "0000-0002-4356-4522"; //vieira
//        private const string urlParameter4 = "0000-0003-4186-7332"; //rosalino

//        static void Main(string[] args)
//        {

//            List<Work> send = new List<Work>();
//            List<Work> works = GetWorks();
//            works.ForEach(w => {
//                if (w.country != null)
//                    Console.WriteLine(w.country.value);
//            });
//            int i = 0;
//            /* if (works.Count != 0)
//             {
//                 //send = works.GetRange(0, 35);
//                 foreach (Work d in works)
//                 {
//                     /*if (d.Citation != null)
//                     {
//                         Console.WriteLine(d.Citation.CitationValue);
//                         i++;
//                     }*/
//            /*List<ExternalId> eids = d.ExternalIds.ExternalId;
//            eids.ForEach(ei => {
//                if (ei.ExternalIdType == "doi")
//                {
//                    i++;
//                    Console.WriteLine(ei.ExternalIdValue);
//                }
//            });
//        }
//    }*/

//            Console.WriteLine("\nDOIs: " + i);
//            //Console.WriteLine("\nCitations: "+ i);
//            Console.WriteLine("PublicationsTotal: " + works.Count);

//            Console.WriteLine("/n/n-------------------- POST ----------------------\n\n");
//            //List<Work> dif = GetDifWorks(send);
//            //Console.WriteLine("PublicationsDif: " + dif.Count);
//            List<Product> ria = GetProducts();
//            Console.WriteLine(ria.Count);
//            List<Work> worksRia = ConvertProductToWork(ria);
//            /*foreach(Work x in w)
//            {
//                Console.WriteLine(x.Title.TitleTitle);
//                Console.WriteLine(x.Url);
//                Console.WriteLine("Year: " + x.PublicationDate.Year+ " Month: " + x.PublicationDate.Month+ " Day: " + x.PublicationDate.Day);
//                Console.WriteLine(x.Type+"\t"+x.LanguageCode);
//                x.ExternalIds.ExternalId.ForEach(eid => Console.WriteLine(eid.ExternalIdType + "\t" + eid.ExternalIdValue + "\t" + eid.ExternalIdRelationship));
//                x.Contributors.Contributor.ForEach(c => Console.WriteLine(c.CreditName + "\t" + c.ContributorAttributes.ContributorRole));
//                Console.WriteLine();
//            }*/
//            List<Work> dif = GetDifWorks(worksRia);
//            Console.WriteLine("PublicationsDif: " + dif.Count);
//        }

//        private static List<Work> GetWorks()
//        {
//            HttpClient client = new HttpClient();
//            client.BaseAddress = new Uri(URL);

//            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//            HttpResponseMessage response = client.GetAsync(urlParameter4).Result;

//            if (response.IsSuccessStatusCode)
//            {
//                var res = response.Content.ReadAsStringAsync().Result;
//                return JsonConvert.DeserializeObject<List<Work>>(res);
//            }
//            else
//            {
//                Console.WriteLine("----------ERROR----------");
//                return new List<Work>();
//            }
//        }

//        private static List<Work> GetDifWorks(List<Work> locals)
//        {
//            HttpClient client2 = new HttpClient();
//            client2.BaseAddress = new Uri(importDifURL);

//            client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//            var json = JsonConvert.SerializeObject(locals);
//            Console.WriteLine(json);
//            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
//            HttpResponseMessage response2 = client2.PostAsync(urlParameter4, stringContent).Result;
//            if (response2.IsSuccessStatusCode)
//            {
//                var resultado = response2.Content.ReadAsStringAsync().Result;
//                return JsonConvert.DeserializeObject<List<Work>>(resultado);
//            }
//            else
//            {
//                Console.WriteLine("\nERROR POST");
//                return new List<Work>();
//            }
//        }

//        public static List<Work> ConvertProductToWork(List<Product> ria)
//        {
//            List<Work> works = new List<Work>();
//            string handleBaseUrl = "http://hdl.handle.net/";
//            foreach (Product p in ria)
//            {
//                Work w = new Work();
//                Title title = new Title(p.Name);
//                w.title = title;
//                w.url = new Url(handleBaseUrl + p.Handle);
//                ExternalIds externalIds = new ExternalIds(new List<ExternalId>());
//                ExternalId eid = new ExternalId("handle", p.Handle, ExternalIdRelationship.SELF.ToString());
//                externalIds.externalId.Add(eid);
//                w.externalIds = externalIds;

//                Contributors cont = new Contributors(new List<Contributor>());
//                foreach (Metadatum data in p.Metadata)
//                {
//                    if (data.Key.Equals("dc.date.issued"))
//                    {
//                        string[] date = data.Value.Split("-");
//                        PublicationDate pd = new PublicationDate();
//                        pd.year = new Year(date[0]);
//                        if (date.Length > 1)
//                        {
//                            pd.month = new Month(date[1]);
//                            if (date.Length == 3)
//                                pd.day = new Day(date[2]);
//                        }
//                        w.publicationDate = pd;
//                    }
//                    else if (data.Key.Equals("dc.type"))
//                    {
//                        foreach (string type in Enum.GetNames(typeof(TypeEnum)))
//                        {
//                            if (data.Value.ToUpper().Equals(type))
//                                w.type = type;
//                        }
//                        if (w.type is null)
//                            w.type = TypeEnum.OTHER.ToString();

//                        foreach (string pais in Enum.GetNames(typeof(CountryType)))
//                        {
//                            if (data.Language.ToUpper().Contains(pais))
//                                w.languageCode = pais;
//                        }
//                    }
//                    else //(data.Key.Equals(Key.DcContributorAdvisor))
//                    {
//                        Contributor contributor = new Contributor(new CreditName(data.Value), new ContributorAttributes(data.Key.Substring(15)));
//                        cont.contributor.Add(contributor);
//                    }

//                }
//                w.contributors = cont;
//                works.Add(w);
//            }
//            return works;
//        }

//        public static List<Product> GetProducts()
//        {
//            Person pessoa = new Person("ua.dados.iupi", "pt_PT");
//            pessoa.Value = "50ca9826-f8c9-46aa-a9b7-10d03dc63f39"; //"66c74f1f-8c45-4f43-9a85-be4975eecc09";

//            HttpClient client = new HttpClient();
//            client.BaseAddress = new Uri("https://ria.ua.pt/");
//            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//            var person = JsonConvert.SerializeObject(pessoa);
//            HttpResponseMessage queryResult = client.PostAsync("rest/items/find-by-metadata-field", new StringContent(person, Encoding.UTF8, "application/json")).Result;

//            if (!queryResult.IsSuccessStatusCode || queryResult.Content == null) { return new List<Product>(); }
//            return JsonConvert.DeserializeObject<List<Product>>(queryResult.Content.ReadAsStringAsync().Result);
//        }
//    }
//}
