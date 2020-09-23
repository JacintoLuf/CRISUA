using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_2020_Business.Models
{
    public class UnidadeInvestigacao
    {
        //Table OrgUnit
        public int OrgUnitId { get; set; }
        public string Acronym { get; set; }
        public string URI { get; set; }

        //Table OrgUnit_Classification
        public int ClassificationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Fraction { get; set; }

        //Table OrgUnit_Identifier
        public int IdentifierId { get; set; }
        public string Value { get; set; }

        //Table OrgUnit_OrgUnit
        public int OrgUnitId2 { get; set; }

        //Table OrgUnit_PAddress
        public int PAddressId { get; set; } 
        
        //Table OrgUnitActivity
        public int LanguageId { get; set; }
        public string Text { get; set; }

        //Table OrgUnitKeywords
        public string Keywords { get; set; }

        //Table OrgUnitName
        public string Name { get; set; }

    }
}
