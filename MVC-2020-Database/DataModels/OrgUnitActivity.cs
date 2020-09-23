using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class OrgUnitActivity
    {
        [Key, Column(Order = 0)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("OrgUnit")]
        public int OrgUnitId { get; set; }

        [Key, Column(Order = 1)]
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Language")]
        public int LanguageId { get; set; }
        public string Text { get; set; }
    }
}
