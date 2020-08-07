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
    public class Person_Publication
    {
        [Key, Column(Order=0)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Person")]
        public int PersonId { get; set; }

        [Key, Column(Order = 1)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Publication")]
        public int PublicationId{ get; set; }


        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Classification")]
        public int ClassificationId{ get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate{ get; set; }
        
        public int Fraction { get; set; }
        public int Order_1 { get; set; }
        public string Copyright { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PersonName")]
        public int PersonNameId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Visibility")]
        public int VisibilityId { get; set; }



    }
}
