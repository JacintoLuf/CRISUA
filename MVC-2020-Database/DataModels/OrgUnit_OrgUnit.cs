using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class OrgUnit_OrgUnit
    {
        [Key, Column(Order = 0)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("OrgUnit")]
        public int OrgUnitId1 { get; set; }

        [Key, Column(Order = 1)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("OrgUnit")]
        public int OrgUnitId2 { get; set; }

        [Key, Column(Order = 2)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Classification")]
        public int ClassificationID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public float Fraction { get; set; }
    }
}
