using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class Publication_Identifier
    {
        [Key, Column(Order =0)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Publication")]
        public int PublicationId { get; set; }

        [Key, Column(Order =1)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Identifier")]
        public int IdentifierId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Key]
        public string Value { get; set; }
    }
}
