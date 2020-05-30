using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class Publication
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [PrimaryKey]
        public int PublicationId { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public bool Synced { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Language")]
        public int LanguageId { get; set; }
        public int State { get; set; }
        public string Type { get; set; }
    }
}
