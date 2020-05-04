using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class Publication_Identifier
    {
        [Key, PrimaryKey]
        public int PublicationId { get; set; }
        [PrimaryKey]
        public int IdentifierId { get; set; }
        [PrimaryKey]
        public DateTime StartDate { get; set; }
        [PrimaryKey]
        public DateTime EndDate { get; set; }
        public string Value { get; set; }
    }
}
