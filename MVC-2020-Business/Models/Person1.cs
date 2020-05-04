using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_2020_Business.Models

{
    public class Person1
    {
        public Person1(string key, string value, string language)
        {
            this.key = key;
            this.value = value;
            this.language = language;
        }
        public string key { get; set; }
        public string value { get; set; }
        public string language { get; set; }
    }
}
