using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_2020_Database.DataModels
{
    public class OrgUnit_Publication
    {


        [Key, Column(Order = 0)]
        public int OrgUnitId { get; set; }
        
        [Key, Column(Order =1)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Publication")]
        public int PublicationId { get; set; }
        public int ClassificationId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Order_1 { set; get; }
        public double Fraction { set; get; }
        public string Copyright { get; set; }
    }
}
