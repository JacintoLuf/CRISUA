using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class Language
    {
        [PrimaryKey, AutoIncrement]
        public int LanguageID { get; set; }
        public string Acronym { get; set; }
        public string Description { get; set; }
    }
}
