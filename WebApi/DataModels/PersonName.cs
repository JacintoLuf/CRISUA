using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class PersonName
    {
        [PrimaryKey, AutoIncrement]
        public int PersonNameId { get; set; }
        public int PersonId{ get; set; }
        public string Name { get; set; }
        public int ClassificationId{ get; set; }
        public DateTime endDate{ get; set; }
        public DateTime startDate{ get; set; }



    }
}
