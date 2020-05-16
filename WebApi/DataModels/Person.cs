using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [PrimaryKey]
        public int PersonID { get; set; }
        public DateTime? BirthDate { get; set; }
        public int GenderId { get; set; }
        public string Photo { get; set; }
    }
}
