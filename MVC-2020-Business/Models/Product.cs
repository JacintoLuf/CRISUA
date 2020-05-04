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
    }
}
