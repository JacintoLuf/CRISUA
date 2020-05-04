using MVC_2020_Business.Models.Orcid;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MVC_2020_Business.Models
{
    public partial class Person2
    {
        public Person2(string key, string language)
        {
            Key = key;
            Language = language;
        }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public partial class Product3
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("handle")]
        public string Handle { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("expand")]
        public List<Expand> Expand { get; set; }

        [JsonProperty("lastModified")]
        public DateTimeOffset LastModified { get; set; }

        [JsonProperty("parentCollection")]
        public object ParentCollection { get; set; }

        [JsonProperty("parentCollectionList")]
        public object ParentCollectionList { get; set; }

        [JsonProperty("parentCommunityList")]
        public object ParentCommunityList { get; set; }

        [JsonProperty("metadata")]
        public List<Metadatum> Metadata { get; set; }

        [JsonProperty("bitstreams")]
        public object Bitstreams { get; set; }

        [JsonProperty("archived")]
        public string Archived { get; set; }

        [JsonProperty("withdrawn")]
        public string Withdrawn { get; set; }

        //public Artigos[] metadata { get; set; }

        public List<Metadatum> Contributors
        {
            get { return Metadata.Where(t => t.Key.Contains("dc.contributor")).ToList(); }
        }

        public override string ToString() => System.Text.Json.JsonSerializer.Serialize<Product3>(this);

    }

    public partial class Metadatum
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public enum Expand { All, Bitstreams, Metadata, ParentCollection, ParentCollectionList, ParentCommunityList };
    public enum Key { DcContributorAdvisor, DcContributorAuthor, DcDateIssued, DcType };



    //public class PublicacoesRIA_Orcid
    //{
    //    public Product RIA { get; set; }
    //    public Works Orcid { get; set; }
    //}
}