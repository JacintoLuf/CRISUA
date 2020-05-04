using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class Publication
    {
        [PrimaryKey, AutoIncrement]
        public int PublicationId { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public bool Synced { get; set; }
        public int LanguageId { get; set; }
    }
}
