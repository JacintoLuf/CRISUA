using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_2020_Business.Models
{
    //public class PublicacaoPortal
    //{
    //    public string Title { get; set; }
    //    public string Source { get; set; }
    //    public string Doi { get; set; }
    //    public string Handle { get; set; }
    //    public string URI { get; set; }
    //    public List<Autor> Authors { get; set; }
    //}


    public class Rootobject
    {
        public Tipo[] data { get; set; }
    }

    public class Tipo
    {
        public string title { get; set; }
        public int id { get; set; }
        public PublicationPortal[] publications { get; set; }
    }

    public class PublicationPortal
    {
        public string year { get; set; }
        public Author[] authors { get; set; }
        public TitlePortal title { get; set; }
        public Origin origin { get; set; }
    }

    public class TitlePortal
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class Origin
    {
        public string label { get; set; }
        public string link { get; set; }
    }

    public class Author
    {
        public string name { get; set; }
        public string title { get; set; }
        public string iupi { get; set; }
    }

}
