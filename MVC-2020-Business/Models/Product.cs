using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVC_2020_Business.Models
{
    public class Product //metodo do pedro lobo
    {
        [JsonProperty("handle")]
        public string Handle { get; set; }

        [JsonProperty("date_issued")]
        public string DateIssued { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("publisher", NullValueHandling = NullValueHandling.Ignore)]
        public string Publisher { get; set; }

        [JsonProperty("doi", NullValueHandling = NullValueHandling.Ignore)]
        public string Doi { get; set; }

        //public string handle { get; set; }
        //public string doi { get; set; }
        //public string date_issued { get; set; }
        //public string title { get; set; }
        //public string publisher { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("abstractText")]
        public string AbstractText { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }

        [JsonProperty("issue")]
        public string Issue { get; set; }

        [JsonProperty("startPage")]
        public string StartPage { get; set; }

        [JsonProperty("endPage")]
        public string EndPage { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("isbn")]
        public string Isbn { get; set; }

        [JsonProperty("issn")]
        public string Issn { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("otherAuthors")]
        public List<AutorRia> OtherAuthors { get; set; }


    }

    public class AutorRia
    {
        public string fullName { get; set; }

        public string iupi { get; set; }
    }
}