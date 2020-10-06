using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class Person_OrgUnit
    {
        [Key, Column(Order = 0)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PersonID")]
        public int PersonID { get; set; }

        [Key, Column(Order = 1)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("OrgUnitId")]
        public int OrgUnitId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ClassificationId")]
        public int ClassificationId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Fraction { get; set; }
        public int VisibilityId { get; set; }


    }
}
