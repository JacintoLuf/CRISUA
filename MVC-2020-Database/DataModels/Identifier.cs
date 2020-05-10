using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class Identifier
    {
        [Key]
        [PrimaryKey, AutoIncrement]
        public int IdentifierId { get; set; }
        public string Term { get; set; }
        public int ClassificationId { get; set; }
    }
}
