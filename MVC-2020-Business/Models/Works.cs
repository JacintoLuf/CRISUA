using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MVC_2020_Business.Models.Orcid
{
    public class LastModifiedDate
    {
        [JsonProperty("value")]
        public long Value { get; set; }

        public DateTime Date
        {
            get { return Helpers.IntToDateTime(Value); }
        }
    }

    public class ExternalIdUrl
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ExternalId
    {
        [JsonProperty("external-id-type")]
        public string ExternalIdType { get; set; }

        [JsonProperty("external-id-value")]
        public string ExternalIdValue { get; set; }

        [JsonProperty("external-id-url")]
        public ExternalIdUrl ExternalIdUrl { get; set; }

        [JsonProperty("external-id-relationship")]
        public string ExternalIdRelationship { get; set; }
    }

    public class ExternalIds
    {
        [JsonProperty("external-id")]
        public IList<ExternalId> ExternalId { get; set; }
    }

    public class CreatedDate
    {
        [JsonProperty("value")]
        public long Value { get; set; }

        public DateTime Date
        {
            get { return Helpers.IntToDateTime(Value); }
        }
    }

    public class SourceOrcid
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }
    }

    public class SourceClientId
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }
    }

    public class SourceName
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Source
    {
        [JsonProperty("source-orcid")]
        public SourceOrcid SourceOrcid { get; set; }

        [JsonProperty("source-client-id")]
        public SourceClientId SourceClientId { get; set; }

        [JsonProperty("source-name")]
        public SourceName SourceName { get; set; }
    }

    public class Title_Title
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Title_Subtitle
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Title_TranslatedTitle
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("language-code")]
        public string LanguageCode { get; set; }
    }

    public class TitleObj
    {
        [JsonProperty("title")]
        public Title_Title Title { get; set; }

        [JsonProperty("subtitle")]
        public Title_Subtitle Subtitle { get; set; }

        [JsonProperty("translated-title")]
        public Title_TranslatedTitle TranslatedTitle { get; set; }
    }

    public class PublicationDate_Year
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class PublicationDate_Month
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class PublicationDate_Day
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class PublicationDate
    {
        [JsonProperty("year")]
        public PublicationDate_Year Year { get; set; }

        [JsonProperty("month")]
        public PublicationDate_Month Month { get; set; }

        [JsonProperty("day")]
        public PublicationDate_Day Day { get; set; }

        [JsonProperty("media-type")]
        public string MediaType { get; set; }
    }

    public class WorkSummary
    {
        [JsonProperty("put-code")]
        public int PutCode { get; set; }

        [JsonProperty("created-date")]
        public CreatedDate CreatedDate { get; set; }

        [JsonProperty("last-modified-date")]
        public LastModifiedDate LastModifiedDate { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("title")]
        public TitleObj Title { get; set; }

        [JsonProperty("external-ids")]
        public ExternalIds ExternalIds { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("publication-date")]
        public PublicationDate PublicationDate { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("display-index")]
        public string DisplayIndex { get; set; }
    }

    public class Group
    {
        [JsonProperty("last-modified-date")]
        public LastModifiedDate LastModifiedDate { get; set; }

        [JsonProperty("external-ids")]
        public ExternalIds ExternalIds { get; set; }

        [JsonProperty("work-summary")]
        public IList<WorkSummary> WorkSummary { get; set; }
    }

    [Serializable]
    public class Works
    {
        [JsonProperty("last-modified-date")]
        public LastModifiedDate LastModifiedDate { get; set; }

        [JsonProperty("group")]
        public IList<Group> Group { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public static class Helpers
    {
        public static DateTime IntToDateTime(long input)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(input);
            return new DateTime(1970, 1, 1) + time;
        }
    }
}