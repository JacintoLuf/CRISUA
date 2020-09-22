using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class PortalIdentifier
    {
        [PrimaryKey]
        public int ID { get; set; }

        public int IdentifierId { get; set; }

        public string NomePortal { get; set; }
    }
}
