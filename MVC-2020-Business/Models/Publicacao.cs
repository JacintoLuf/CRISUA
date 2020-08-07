using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_2020_Business.Models
{
    public class Publicacao
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
        public string Abstract { get; set; }
        public string Number { get; set; }
        public string Volume { get; set; }
        public string Edition { get; set; }
        public string Series { get; set; }
        public string Issue { get; set; }
        public string StartPage { get; set; }
        public string EndPage { get; set; }
        public string TotalPages { get; set; }
        public string ISBN { get; set; }
        public string ISSN { get; set; }
        public string Doi { get; set; }
        public string Handle { get; set; }
        public string URI { get; set; }
        public string Journal { get; set; }
        public string Language { get; set; }
        public List<Autor> Authors { get; set; }
    }
}
