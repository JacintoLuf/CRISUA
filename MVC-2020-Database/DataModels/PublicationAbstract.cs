using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class PublicationAbstract
    {
        [Key]
        [PrimaryKey]
        public int PublicationId { get; set; }
        public int LanguageId { get; set; }
        public string Abstract { get; set; }
        
    }
}
