﻿//using BibTeXLibrary;
using Microsoft.EntityFrameworkCore;
using MVC_2020_Business.Models;
using MVC_2020_Database.DataModels;
using ServiceStack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
//using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Visibility = MVC_2020_Database.DataModels.Visibility;

namespace MVC_2020_Business.Services
{
    public class DatabaseServices
    {
        public static void insertPublicationsRIA(MyDbContext _db, List<Product> lista)
        {
            var source = "RIA";
            var p1 = new PublicationTitle() { LanguageId = 4, PublicationId = -1, Title = "as" };
            var p2 = new PublicationTitle() { LanguageId = 4, PublicationId = -1, Title = "as" };

            var arr = new ArrayList();
            var pubs = new List<Publication>();
            var idents = new List<Publication_Identifier>();
            var orgPubs = new List<OrgUnit_Publication>();
            var pubTitles = new List<PublicationTitle>();

            var go = 1;

            var num = _db.Publication.Count();

            foreach (var inp in lista)
            {
                go = 1;


                var queryP = from tit in _db.PublicationTitle where tit.Title == inp.Title select tit.Title;
                if (!(queryP.FirstOrDefault() is null))
                {
                    go = 0;
                }

                var date = inp.DateIssued;
                if (date.Length < 10)
                    date = DateTime.Now.ToString();

                if (go == 1)
                {
                    num++;
                    pubs.Add(new Publication() { Date = DateTime.Parse(date), LanguageId = 3, Source = source, Synced = false, State = 1 });

                    var doi = inp.Doi;
                    var handle = inp.Handle;
                    var publisher = inp.Publisher;
                    var title = inp.Title;

                    if (doi is null)
                    {
                        idents.Add(new Publication_Identifier() { EndDate = DateTime.MaxValue, IdentifierId = 2, PublicationId = num, StartDate = DateTime.Parse(date), Value = handle });
                    }
                    else
                    {
                        idents.Add(new Publication_Identifier() { EndDate = DateTime.MaxValue, IdentifierId = 1, PublicationId = num, StartDate = DateTime.Parse(date), Value = doi });
                        idents.Add(new Publication_Identifier() { EndDate = DateTime.MaxValue, IdentifierId = 2, PublicationId = num, StartDate = DateTime.Parse(date), Value = handle });

                    }


                    orgPubs.Add(new OrgUnit_Publication() { StartDate = DateTime.Parse(date), ClassificationId = 5, Copyright = "", EndDate = DateTime.MaxValue, Fraction = 0, Order_1 = 0, OrgUnitId = 1, PublicationId = num });

                    var pub_title = new PublicationTitle()
                    {
                        PublicationId = num,
                        LanguageId = 2,
                        Title = title
                    };

                    if (title == "Impact of the transient behavior of radio communication systems on spectrum management" && p1.LanguageId == 4) p1 = pub_title;
                    else if (title == "Impact of the transient behavior of radio communication systems on spectrum management" && p2.LanguageId == 4) p2 = pub_title;

                    if (arr.Contains(title)) { }
                    else
                    {

                        var p3 = p1;
                        var p4 = p2;
                        arr.Add(title);
                        pubTitles.Add(pub_title);
                    }
                }
            }



            _db.Set<Publication>().AddRange(pubs);
            _db.SaveChanges();
            _db.Set<Publication_Identifier>().AddRange(idents);
            _db.Set<OrgUnit_Publication>().AddRange(orgPubs);
            _db.Set<PublicationTitle>().AddRange(pubTitles);

            _db.SaveChanges();
        }

        public static void insertPublicationsPTCRIS(MyDbContext _db, string nome, List<Work> lista)
        {

            nome = "José Manuel Neto Vieira";
            var source = "ORCID";
            var pubs = new List<Publication>();
            var pers = new List<Person>();
            var names = new List<PersonName>();
            var details = new List<PublicationDetail>();
            var pers_pub = new List<Person_Publication>();
            var idents = new List<Publication_Identifier>();
            var orgPubs = new List<OrgUnit_Publication>();
            var pubTitles = new List<PublicationTitle>();
            var visi = new List<Visibility>();


            var arr = new ArrayList();
            var contNames = _db.PersonName.Count();
            var contPer = _db.Person.Count();
            var contPub = _db.Publication.Count();
            var issn = "";
            var go = 1;
            int max = 0;

            var principais = new List<string>();

            foreach (var inp in lista)
            {
                go = 1;
                max = 0;
                String principal = "";

                //PESSOAS
                if (!(inp.contributors is null))
                {
                    var contri = inp.contributors.contributor.Select(x => x.creditName.value).ToList();
                    foreach (var c in contri)
                    {

                        if (checkSim(nome, c) > max)
                        {
                            max = checkSim(nome, c);
                            principal = c;
                        }
                    }

                    if (!principais.Contains(principal))
                        principais.Add(principal);

                    foreach (var c2 in contri)
                    {

                        if (c2 != principal)
                        {
                            //var n = c2.Trim();
                            if (!arr.Contains(c2))
                            {
                                arr.Add(c2);

                                var queryN = from tmp in _db.PersonName where tmp.Name == c2 select tmp.Name;
                                var tmp_r = queryN.FirstOrDefault();

                                if (tmp_r is null)
                                {
                                    contPer++;
                                    names.Add(new PersonName() { ClassificationId = 2, endDate = DateTime.MaxValue, Name = c2, PersonId = contPer, startDate = DateTime.Now });
                                    pers.Add(new Person() { BirthDate = null, GenderId = 3, Photo = null });
                                }
                            }
                        }
                    }
                }

                //PUBLICACOES      

                var queryP = from tit in _db.PublicationTitle where tit.Title == inp.title.title select tit.Title;
                if (!(queryP.FirstOrDefault() is null))
                {
                    go = 0;
                }



                if (go == 1)
                {
                    contPub++;
                    var tmp0 = inp.publicationDate.ToString();
                    var date = DateTime.Parse(tmp0);
                    source = inp.source.sourceName.content;

                    pubs.Add(new Publication() { Date = date, LanguageId = 3, Source = source, Synced = false, State = 1 });

                    //IDENTIFIERS

                    foreach (var ex in inp.externalIds.externalId)
                    {
                        if (ex.externalIdType.EqualsIgnoreCase("DOI"))
                            idents.Add(new Publication_Identifier() { EndDate = DateTime.MaxValue, IdentifierId = 1, PublicationId = contPub, StartDate = DateTime.Now, Value = ex.externalIdValue });
                        else if (ex.externalIdType.EqualsIgnoreCase("issn"))
                        {
                            issn = ex.externalIdValue;
                        };
                    }


                    //TITULOS
                    pubTitles.Add(new PublicationTitle() { LanguageId = 3, PublicationId = contPub, Title = inp.title.title });


                    ///DETAILS

                    if (inp.citation != null)
                    {
                        if (inp.citation.citationType == "BIBTEX")
                        {
                            //Console.WriteLine(inp.citation.citationValue + "\n");
                            //Console.WriteLine("Number: " + Regex.Match(inp.citation.citationValue, @"number = {(.+?)}").Groups[1].Value);
                            //Console.WriteLine("Volume: " + Regex.Match(inp.citation.citationValue, @"volume = {(.+?)}").Groups[1].Value);
                            //Console.WriteLine("pages: " + Regex.Match(inp.citation.citationValue, @"pages = {(.+?)}").Groups[1].Value);
                            //Console.WriteLine("Journal: " + Regex.Match(inp.citation.citationValue, @"journal = {(.+?)}").Groups[1].Value);
                            //Console.WriteLine("Publisher: " + Regex.Match(inp.citation.citationValue, @"publisher = {(.+?)}").Groups[1].Value);
                            //Console.WriteLine("ISBN: " + Regex.Match(inp.citation.citationValue, @"isbn = {(.+?)}").Groups[1].Value);

                            //                                                      ABSTRACT
                            //Console.WriteLine("Abstract: " + Regex.Match(w.citation.citationValue, "abstract\\s*=\\s*({|\")(.+?)(}|\")").Groups[2].Value);

                            //Console.WriteLine("--------");

                            var pag1 = "";
                            var pag2 = "";
                            var difPag = "";


                            var paginas = Regex.Match(inp.citation.citationValue, "pages\\s*=\\s*({|\")(.+?)(}|\")").Groups[2].Value;
                            if (paginas.Contains("-"))
                            {
                                var arrPag = paginas.Split("-");
                                pag1 = arrPag[0];
                                pag2 = arrPag[arrPag.Length - 1];
                                difPag = (System.Math.Abs((getNumber(pag2) - getNumber(pag1)))) + "";
                            }

                            details.Add(new PublicationDetail()
                            {
                                PublicationId = contPub,
                                Number = Regex.Match(inp.citation.citationValue, "number\\s*=\\s*({|\")(.+?)(}|\")").Groups[2].Value,
                                Volume = Regex.Match(inp.citation.citationValue, "volume\\s*=\\s*({|\")(.+?)(}|\")").Groups[2].Value,
                                StartPage = pag1,
                                EndPage = pag2,
                                TotalPages = difPag,
                                ISBN = Regex.Match(inp.citation.citationValue, "isbn\\s*=\\s*({|\")(.+?)(}|\")").Groups[2].Value,
                                ISSN = issn
                            });
                        }
                        issn = "";
                    }
                }

            }


            _db.Set<Publication>().AddRange(pubs);
            _db.SaveChanges();
            _db.Set<Person>().AddRange(pers);
            _db.SaveChanges();
            _db.Set<PersonName>().AddRange(names);
            _db.Set<Publication_Identifier>().AddRange(idents);
            _db.Set<PublicationTitle>().AddRange(pubTitles);
            _db.SaveChanges();


            var firstPrin = principais.First();

            var query1 = from tmp in _db.PersonName
                         join tmp2 in _db.Person on tmp.PersonId equals tmp2.PersonID
                         where tmp.Name.Equals(firstPrin)
                         select tmp2.PersonID;

            var secondPrin = principais.ToArray()[1];

            var query2 = from tmp in _db.PersonName
                         where tmp.PersonId == query1.FirstOrDefault()
                         select tmp.PersonNameId;

            if (query2.ToList().Count < 2)
            {
                _db.Set<Person>().Add(new Person() { BirthDate = null, GenderId = 3, Photo = null });
                _db.SaveChanges();

                foreach (var p in principais)
                {
                    _db.Set<PersonName>().Add(new PersonName() { ClassificationId = 2, endDate = DateTime.MaxValue, Name = p, PersonId = _db.Person.Count(), startDate = DateTime.Now });
                }
                _db.SaveChanges();
            }

            //////


            foreach (var inp in lista)
            {

                if (!(inp.contributors is null))
                {

                    var qId = from pub in _db.PublicationTitle where pub.Title == inp.title.title select pub.PublicationId;
                    var id = qId.FirstOrDefault();

                    var qTest = from data in _db.Person_Publication where data.PublicationId == id select data.PersonId;
                    var trig = qTest.FirstOrDefault();

                    if (trig == 0)
                    {

                        var contri = inp.contributors.contributor.Select(x => x.creditName.value).ToList();
                        foreach (var c in contri)
                        {
                            if (principais.Contains(c))
                            {
                                var qp = from p in _db.PersonName where p.Name == c select p.PersonNameId;
                                var ppName = qp.FirstOrDefault();
                                pers_pub.Add(new Person_Publication() { ClassificationId = 2, Copyright = null, endDate = DateTime.MaxValue, Fraction = 100 / contri.Count, Order_1 = 1, PublicationId = id, startDate = DateTime.Now, VisibilityId = 2, PersonId = _db.Person.Count(), PersonNameId = ppName });

                            }
                            else
                            {
                                //PERSON_PUB
                                var q1 = from p in _db.PersonName where p.Name == c select p.PersonId;
                                var per = q1.FirstOrDefault();

                                var q2 = from p in _db.PersonName where p.Name == c select p.PersonNameId;
                                var pName = q2.FirstOrDefault();

                                pers_pub.Add(new Person_Publication() { ClassificationId = 2, Copyright = null, endDate = DateTime.MaxValue, Fraction = 100 / contri.Count, Order_1 = 1, PublicationId = id, startDate = DateTime.Now, VisibilityId = 2, PersonId = per, PersonNameId = pName });
                            }
                        }
                    }
                }
            }

            _db.Set<Person_Publication>().AddRange(pers_pub);
            _db.SaveChanges();


        }

        public static Hashtable retrieveAllInfo(MyDbContext _db, string titulo)
        {
            Hashtable map = new Hashtable();

            var query = from tmp in _db.PublicationTitle where tmp.Title == titulo select tmp.PublicationId;
            var id = query.FirstOrDefault();

            map.Add("titulo", titulo);

            var queryDOI = from tmp in _db.Publication_Identifier where tmp.PublicationId == id & tmp.IdentifierId == 1 select tmp.Value;
            map.Add("DOI", queryDOI.FirstOrDefault());

            var queryExt = from tmp in _db.Publication_Identifier where tmp.PublicationId == id & tmp.IdentifierId != 1 select tmp.Value;
            map.Add("Identificadores Externos", queryExt.FirstOrDefault());

            var queryAuth = from tmp in _db.Person_Publication
                            join person in _db.PersonName
                            on tmp.PersonId equals person.PersonId
                            where tmp.PublicationId == id
                            select person.Name;

            var a = queryAuth.ToList();
            map.Add("Autores", queryAuth.ToList());

            var queryData = from tmp in _db.Publication where tmp.PublicationId == id select tmp.Date;
            string data_hora = queryData.FirstOrDefault().ToString();

            map.Add("Data", Partir_data(data_hora)[0]);
            map.Add("Dia", Partir_data(data_hora)[1]);
            map.Add("Mes", Partir_data(data_hora)[2]);
            map.Add("Ano", Partir_data(data_hora)[3]);


            var queryState = from tmp in _db.Publication where tmp.PublicationId == id select tmp.State;
            var qS = queryState.FirstOrDefault();
            switch (qS)
            {
                case 1:
                    map.Add("Estado", "Importada");
                    break;
                case 2:
                    map.Add("Estado", "Aceite");
                    break;
                case 3:
                    map.Add("Estado", "Em análise");
                    break;
                case 4:
                    map.Add("Estado", "Pronta a importar para o RIA");
                    break;
                case 5:
                    map.Add("Estado", "Importada no RIA");
                    break;
                case 6:
                    map.Add("Estado", "Validada pela Biblioteca");
                    break;
                default:
                    map.Add("Estado", "NULL");
                    break;
            }


            var queryLng = from tmp in _db.Publication where tmp.PublicationId == id select tmp.LanguageId;
            var qL = from l in _db.Language where l.LanguageID == queryLng.FirstOrDefault() select l.Acronym;
            map.Add("Language", qL.FirstOrDefault());

            var queryNumState = from tmp in _db.Publication where tmp.PublicationId == id select tmp.State;
            map.Add("State(numero)", queryState.FirstOrDefault().ToString()); ;

            return map;
        }

        public static List<String> select(MyDbContext _db, string tabela, string coluna, string valor)
        {
            string var = tabela;

            switch (var)
            {
                //case "Person":
                //    selectPerson(_db, coluna, valor);
                //    break;

                case "Publication":
                    List<String> ret = new List<string>();
                    int hlp = Int32.Parse(valor);

                    var query = from tmp in _db.Publication
                                where tmp.State == hlp
                                join tit in _db.PublicationTitle on tmp.PublicationId equals tit.PublicationId
                                select tit.Title;

                    foreach (var i in query.ToList())
                    {
                        ret.Add(i);
                    }

                    return ret;

                    //case "PublicationTitle":
                    //    selectPublicationTitle(_db, coluna, valor);
                    //    break;

                    //case "PublicationIdentifier":
                    //    selectPublicationIdentifier(_db, coluna, valor);
                    //    break;



            }

            return null;
        }

        public static List<String> selectAllPubsInBD(MyDbContext _db, string tabela, string coluna)
        {
            string var = tabela;
            switch (var)
            {
                case "Publication":
                    List<String> ret = new List<string>();

                    var query = from tmp in _db.Publication
                                join tit in _db.PublicationTitle on tmp.PublicationId equals tit.PublicationId
                                select tit.Title;

                    foreach (var i in query.ToList())
                    {
                        ret.Add(i);
                    }
                    return ret;
            }
            return null;
        }

        //private static void selectPublicationIdentifier(MyDbContext db, string coluna, string valor)
        //{
        //    throw new NotImplementedException();
        //}

        //private static void selectPublicationTitle(MyDbContext db, string coluna, string valor)
        //{
        //    throw new NotImplementedException();
        //}

        //private static void selectPerson(MyDbContext db, string coluna, string valor)
        //{
        //    throw new NotImplementedException();
        //}

        public static void updateState(MyDbContext _db, string titulo, int valor)
        {
            bool f = false;

            var query = from tit in _db.PublicationTitle where tit.Title == titulo select tit.PublicationId;

            Publication a = new Publication();
            a = _db.Publication.Find(query.FirstOrDefault());
            a.State = valor;
            _db.Entry(a).State = EntityState.Modified;
            _db.SaveChanges();

        }

        public static List<Hashtable> selectToRIA(MyDbContext _db, List<string> titulos)
        {

            Hashtable map;
            List<Hashtable> map2 = new List<Hashtable>();

            foreach (var titulo in titulos)
            {
                map = new Hashtable();
                var query = from tmp in _db.PublicationTitle where tmp.Title == titulo select tmp.PublicationId;
                var id = query.FirstOrDefault();

                map.Add("titulo", titulo);

                var queryISSN = from tmp in _db.PublicationDetail
                                join pub in _db.Publication
                                on tmp.PublicationId equals pub.PublicationId
                                select tmp.ISSN;
                map.Add("ISSN", queryISSN.FirstOrDefault());

                var queryStartPage = from tmp in _db.PublicationDetail
                                     join pub in _db.Publication
                                     on tmp.PublicationId equals pub.PublicationId
                                     select tmp.StartPage;
                map.Add("StartPage", queryStartPage.FirstOrDefault());

                var queryEndPage = from tmp in _db.PublicationDetail
                                   join pub in _db.Publication
                                   on tmp.PublicationId equals pub.PublicationId
                                   select tmp.EndPage;
                map.Add("EndPage", queryEndPage.FirstOrDefault());

                var queryTotalPages = from tmp in _db.PublicationDetail
                                      join pub in _db.Publication
                                      on tmp.PublicationId equals pub.PublicationId
                                      select tmp.TotalPages;
                map.Add("TotalPages", queryTotalPages.FirstOrDefault());

                var queryVolume = from tmp in _db.PublicationDetail
                                  join pub in _db.Publication
                                  on tmp.PublicationId equals pub.PublicationId
                                  select tmp.Volume;
                map.Add("Volume", queryVolume.FirstOrDefault());

                var queryEdition = from tmp in _db.PublicationDetail
                                   join pub in _db.Publication
                                   on tmp.PublicationId equals pub.PublicationId
                                   select tmp.Edition;
                map.Add("Edition", queryEdition.FirstOrDefault());


                var querySeries = from tmp in _db.PublicationDetail
                                  join pub in _db.Publication
                                  on tmp.PublicationId equals pub.PublicationId
                                  select tmp.Series;
                map.Add("Series", querySeries.FirstOrDefault());


                var queryDOI = from tmp in _db.Publication_Identifier where tmp.PublicationId == id & tmp.IdentifierId == 1 select tmp.Value;
                map.Add("DOI", queryDOI.FirstOrDefault());

                var queryExt = from tmp in _db.Publication_Identifier where tmp.PublicationId == id & tmp.IdentifierId != 1 select tmp.Value;
                map.Add("Identificadores Externos", queryExt.FirstOrDefault());

                var queryAuth = from tmp in _db.Person_Publication
                                join person in _db.PersonName
                                on tmp.PersonId equals person.PersonId
                                where tmp.PublicationId == id
                                select person.Name;

                var a = queryAuth.ToList();
                map.Add("Autores", queryAuth.ToList());

                var queryData = from tmp in _db.Publication where tmp.PublicationId == id select tmp.Date;
                string data_hora = queryData.FirstOrDefault().ToString();

                map.Add("Data", Partir_data(data_hora)[0]);
                map.Add("Dia", Partir_data(data_hora)[1]);
                map.Add("Mes", Partir_data(data_hora)[2]);
                map.Add("Ano", Partir_data(data_hora)[3]);

                map.Add("Estado", "published");

                var queryLng = from tmp in _db.Publication where tmp.PublicationId == id select tmp.LanguageId;
                var qL = from l in _db.Language where l.LanguageID == queryLng.FirstOrDefault() select l.Acronym;
                //var queryLng2 = from tmp in _db.Publication           //Dário desta forma fazes apenas um pedido à BD, mas depois vê se funciona!
                //                join p in _db.Language on tmp.LanguageId equals p.LanguageID 
                //                select p.Acronym; 

                map.Add("Language", qL.FirstOrDefault());

                var queryFnt = from tmp in _db.Publication where tmp.PublicationId == id select tmp.Source;
                map.Add("Fonte", queryFnt.FirstOrDefault());

                map2.Add(map);
            }

            return map2;
        }

        private static List<String> Partir_data(string data_hora)
        {
            List<String> l = new List<String>();

            string[] s = data_hora.Split(" ");
            string data = s[0];
            string hora = s[1]; /*-- nao usamos--*/

            l.Add(data);

            return l.Concat(data.Split("/").ToList()).ToList(); /*e.g ["1/2/3333", "1", "2", "3333"]*/
        }

        private static void updatePub(MyDbContext _db, string titulo, string info, string valor)
        {

            var query = from titl in _db.PublicationTitle where titl.Title == titulo select titl.PublicationId;
            var id = query.FirstOrDefault();
            if (id > 0)
            {
                switch (info)
                {
                    case "Titulo":
                        PublicationTitle a = new PublicationTitle();
                        a = _db.PublicationTitle.Find(titulo);
                        a.Title = valor;
                        _db.Entry(a).State = EntityState.Modified;
                        _db.SaveChanges();
                        break;

                    case "DOI":

                        Publication_Identifier b = new Publication_Identifier();
                        b = _db.Publication_Identifier.Find(id, 1);
                        b.Value = valor;
                        _db.Entry(b).State = EntityState.Modified;
                        _db.SaveChanges();
                        break;

                    case "ISSN":

                        var queryI = from dois in _db.Publication_Identifier where dois.PublicationId == id && dois.IdentifierId == 2 select dois;
                        var issn = query.FirstOrDefault();

                        Publication_Identifier c = new Publication_Identifier();
                        if (issn == 0)
                        {
                            var queryData = from tmpP in _db.Publication where tmpP.PublicationId == id select tmpP.Date;

                            c = new Publication_Identifier() { Value = valor, IdentifierId = 2, PublicationId = id, EndDate = DateTime.MaxValue, StartDate = queryData.FirstOrDefault() };
                            _db.Set<Publication_Identifier>().AddRange(c);
                            _db.SaveChanges();
                        }
                        else
                        {
                            c = _db.Publication_Identifier.Find(id, 2);
                            c.Value = valor;
                            _db.Entry(c).State = EntityState.Modified;
                            _db.SaveChanges();
                        }
                        break;

                    //case "Autores": CASO ESPECIFICO, MT TRABALHO, FAZER FUNCAO A PARTE

                    //case "Volume":            //A FAZER A SEGUIR
                    //    break;
                    //case "Edicao":
                    //    break;
                    //case "Paginas":
                    //    break;
                    //case "Pagina Inicial":
                    //    break;
                    //case "Pagina Final":
                    //    break;

                    case "Data":

                        Publication d = new Publication();
                        d = _db.Publication.Find(id);
                        d.Date = DateTime.Parse(valor);
                        _db.Entry(d).State = EntityState.Modified;
                        _db.SaveChanges();
                        break;
                }
            }
        }
        //ALGORITMO DE COMPARACAO DE STRINGS
        public static int checkSim(String nome, String autor)
        {
            int pontos = 0;
            String[] arr = nome.Split(" ");

            for (int i = 0; i < arr.Length; i++)
            {
                if (autor.Contains(arr[i])) pontos += 10;
                if (autor.Contains(arr[i][0] + ".")) pontos += 5;
            }
            return pontos;
        }


        public static int getNumber(string a)
        {
            string b = "";

            for (int i = 0; i < a.Length; i++)
            {
                if (Char.IsDigit(a[i]))
                    b += a[i];
            }

            if (b.Length > 0)
                return int.Parse(b);

            return 0;
        }

        //public static void updateOrcid(MyDbContext _db, string iupi)
        //{
        //    var leng= _db.Person.Count();
        //    var query = from tit in _db.Person where tit.Title == titulo select tit.PublicationId;

        //    Publication a = new Publication();
        //    a = _db.Publication.Find(query.FirstOrDefault());
        //    a.State = valor;
        //    _db.Entry(a).State = EntityState.Modified;
        //    _db.SaveChanges();
        //}

        public static string getOrcid(MyDbContext _db, string iupi)
        {
            var query = from tmp in _db.Person_Identifier
                            //join per in _db.Person on tmp.PersonID equals per.PersonID
                        where tmp.Value == iupi && tmp.IdentifierId == 3
                        select tmp.PersonID;

            if (!(query.FirstOrDefault() == 0))
            {
                var query2 = from tmp in _db.Person_Identifier
                             where tmp.PersonID == query.FirstOrDefault() && tmp.IdentifierId == 1
                             select tmp.Value;

                return query2.FirstOrDefault();
            }
            else return null;
        }
    }
}