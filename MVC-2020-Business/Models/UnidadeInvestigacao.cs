using MVC_2020_Database.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_2020_Business.Models
{
    public class UnidadeInvestigacao //nao associa a publicacoes
    {
        //Table OrgUnit
        public int OrgUnitId { get; set; }
        public string Acronym { get; set; }
        public string URI { get; set; }

        //Table OrgUnit_Classification
        public int ClassificationId { get; set; }
        public DateTime ClassStartDate { get; set; }
        public DateTime ClassEndDate { get; set; }
        public double Fraction { get; set; }

        //Table OrgUnit_Identifier
        public int IdentifierId { get; set; }
        public string Value { get; set; }
        public DateTime IDStartDate { get; set; }
        public DateTime IDEndDate { get; set; }

        //Table OrgUnit_OrgUnit
        public int OrgUnitId2 { get; set; }
        public int OG2ClassId { get; set; }
        public double OG2Fraction { get; set; }
        public DateTime OG2StartDate { get; set; }
        public DateTime OG2EndDate { get; set; }

        //Table OrgUnit_PAddress
        public int PAddressId { get; set; }
        public PAddress Address { get; set; }
        public DateTime AddStartDate { get; set; }
        public DateTime AddEndDate { get; set; }

        //Table OrgUnitActivity
        public int ActLanguageId { get; set; }
        public string Text { get; set; }

        //Table OrgUnitKeywords
        public int KwLanguageId { get; set; }
        public string Keywords { get; set; }

        //Table OrgUnitName
        public int NameLanguageId { get; set; }
        public string Name { get; set; }

    }
}
