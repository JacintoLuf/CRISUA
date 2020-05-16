using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class PublicationTitle
    {
        public int PublicationId { get; set; }
        public int LanguageId { get; set; }
        [Key]
        [PrimaryKey]
        public string Title{ get; set; }

    }
}
