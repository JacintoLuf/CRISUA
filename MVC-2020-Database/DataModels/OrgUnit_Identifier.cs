using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class OrgUnit_Identifier
    {
        [Key, Column(Order = 0)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("OrgUnit")]
        public int OrgUnitId { get; set; }

        [Key, Column(Order = 1)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Identifier")]
        public int IdentifierId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Value { get; set; }
    }
}
