using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class Nomes_Portal_ORCID
    {
        [PrimaryKey]
        public string NomeOrcid { get; set; }

        public int ID { get; set; }
    }
}