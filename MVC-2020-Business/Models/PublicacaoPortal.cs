using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_2020_Business.Models
{
    public class PublicacaoPortal
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string Doi { get; set; }
        public string Handle { get; set; }
        public string URI { get; set; }
        public List<Autor> Authors { get; set; }
    }
}
