﻿//using BibTeXLibrary;
using Microsoft.EntityFrameworkCore;
using MVC_2020_Business.Models;
using MVC_2020_Database.DataModels;
using Newtonsoft.Json;
using ServiceStack;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Linq;
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
                    pubs.Add(new Publication() { Date = DateTime.Parse(date), LanguageId = 3, Source = source, Synced = false });

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

        public static void insertPublicationsPTCRIS(MyDbContext _db, List<Work> lista)
        {
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
            var contPer = _db.Person.Count();
            var contPub = _db.Publication.Count();
            var contPub2 = _db.Publication.Count();
            var issn = "";
            var go = 1;



            foreach (var inp in lista)
            {
                go = 1;


                //PESSOAS
                if (!(inp.contributors is null))
                {
                    var contri = inp.contributors.contributor.InArray()[0];
                    foreach (var c in contri)
                    {
                        var n = c.creditName.value.Trim();
                        if (!arr.Contains(n))
                        {

                            contPer++;
                            arr.Add(n);


                            var queryN = from tmp in _db.PersonName where tmp.Name == n select tmp.Name;
                            var tmp_r = queryN.FirstOrDefault();

                            if (tmp_r is null)
                            {
                                names.Add(new PersonName() { ClassificationId = 2, endDate = DateTime.MaxValue, Name = n, PersonId = contPer, startDate = DateTime.Now });
                                pers.Add(new Person() { BirthDate = null, GenderId = 3, Photo = null });
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

                    pubs.Add(new Publication() { Date = date, LanguageId = 3, Source = source, Synced = false });

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

                }

                //var ls = new List<BibEntry>();

                ///DETAILS
                /*if (!(inp.citation is null))
                {
                    if (inp.citation.citationType.EqualsIgnoreCase("BIBTEX"))
                    {
                        while (inp.citation.citationValue.Contains(':'))
                            inp.citation.citationValue.Trim(':');
                        var parser = new BibParser(new StringReader(inp.citation.citationValue));
                        var entry = parser.GetAllResult()[0];
                        //ls.Add(parser.GetAllResult()[0]);

                        var volume = entry.Volume;
                        var edition = entry.Edition;

                        var series = entry.Series;
                        var pages = entry.Pages;
                        var org = entry.Organization;
                        var inst = entry.Institution;

                    }
                }*/



            }

            _db.Set<Publication>().AddRange(pubs);
            _db.SaveChanges();
            _db.Set<Person>().AddRange(pers);
            _db.SaveChanges();
            _db.Set<PersonName>().AddRange(names);
            _db.Set<Publication_Identifier>().AddRange(idents);
            _db.Set<PublicationTitle>().AddRange(pubTitles);
            _db.SaveChanges();


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

                        //contPub2++;


                        var contri = inp.contributors.contributor.InArray()[0];
                        foreach (var c in contri)
                        {
                            //PERSON_PUB
                            var q1 = from p in _db.PersonName where p.Name == c.creditName.value select p.PersonId;
                            var per = q1.FirstOrDefault();

                            var q2 = from p in _db.PersonName where p.Name == c.creditName.value select p.PersonNameId;
                            var pName = q2.FirstOrDefault();

                            pers_pub.Add(new Person_Publication() { ClassificationId = 2, Copyright = null, endDate = DateTime.MaxValue, Fraction = 100 / contri.Count, Order_1 = 1, PublicationId = id, startDate = DateTime.Now, VisibilityId = 2, PersonId = per, PersonNameId = pName });
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
            map.Add("Data", queryData.FirstOrDefault().ToString()); ;

            map.Add("Estado", "published");

            var queryLng = from tmp in _db.Publication where tmp.PublicationId == id select tmp.LanguageId;
            var qL = from l in _db.Language where l.LanguageID == queryLng.FirstOrDefault() select l.Acronym;

            map.Add("Language", qL.FirstOrDefault());


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

                    bool hlp = false;
                    if (valor == "1") hlp = true;

                    var query = from tmp in _db.Publication
                                where tmp.Synced == hlp
                                join tit in _db.PublicationTitle on tmp.PublicationId equals tit.PublicationId
                                select tit.Title;

                    foreach (var i in query.ToList())
                    {
                        ret.Add(i);
                    }

                    return ret;

                    break;

                    //case "PublicationTitle":
                    //    selectPublicationTitle(_db, coluna, valor);
                    //    break;

                    //case "PublicationIdentifier":
                    //    selectPublicationIdentifier(_db, coluna, valor);
                    //    break;



            }

            return null;
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
                map.Add("Data", queryData.FirstOrDefault().ToString()); ;

                map.Add("Estado", "published");

                var queryLng = from tmp in _db.Publication where tmp.PublicationId == id select tmp.LanguageId;
                var qL = from l in _db.Language where l.LanguageID == queryLng.FirstOrDefault() select l.Acronym;

                map.Add("Language", qL.FirstOrDefault());

                map2.Add(map);
            }

            return map2;
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

        public static void updateState(MyDbContext _db, String works, int valor)
        {
            var work = JsonConvert.DeserializeObject<List<Work>>(works);
            foreach (var w in work)
            {


                bool f = false;

                var query = from tit in _db.PublicationTitle where tit.Title == w.title.title select tit.PublicationId;
                Publication a = new Publication();
                a = _db.Publication.Find(query.FirstOrDefault());
                if (valor == 1) f = true;

                a.Synced = f;
                _db.Entry(a).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }
    }
}
