using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class Classification
    {
        [PrimaryKey]
        public int ClassificationId { get; set; }
        public string Term { get; set; }
        public string ClassUUID { get; set; }
    }
}
