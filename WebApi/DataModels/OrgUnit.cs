using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class OrgUnit
    {
        [PrimaryKey]
        public int OrgUnitId { get; set; }
        public string Acronym { get; set; }
        public string URI { get; set; }
    }
}
