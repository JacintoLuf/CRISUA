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
    public class Person_Identifier
    {
        [Key, Column(Order = 0)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Person")]
        public int PersonID { get; set; }

        [Key, Column(Order = 1)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Identifier")]
        public int IdentifierId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Value { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Visibility")]
        public int VisibilityId { get; set; }
    }
}
