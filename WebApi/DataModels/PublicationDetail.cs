using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class PublicationDetail
    {

        //[System.ComponentModel.DataAnnotations.Schema.ForeignKey("Publication")]
        [Key]
        [PrimaryKey]
        public int PublicationId { get; set; }
        public string Number { get; set; }
        public string Volume { get; set; }
        public string Edition { get; set; }
        public string Series { get; set; }
        public string Issue { get; set; }
        public string StartPage{ get; set; }
        public string EndPage{ get; set; }
        public string TotalPages{ get; set; }
        public string ISBN { get; set; }
        public string ISSN { get; set; }
        public string URI { get; set; }
    }
}
