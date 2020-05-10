using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class PersonGender
    {
        [PrimaryKey]
        public int GenderId { get; set; }
        public string Term { get; set; }
    }
}
