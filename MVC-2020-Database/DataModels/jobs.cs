using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVC_2020_Database.DataModels
{
    public class jobs
    {
        [PrimaryKey, AutoIncrement]
        public int job_id { get; set; }
        public string job_desc { get; set; }
        public int min_lvl { get; set; }
        public int max_lvl { get; set; }
    }
}
