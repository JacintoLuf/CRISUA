using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MVC_2020_Business.Models
{
    public class CreatedDate
    {

        [JsonProperty("value")]
        public DateTime value { get; set; }
    }

    public class LastModifiedDate
    {

        [JsonProperty("value")]
        public DateTime value { get; set; }
    }

    public class SourceOrcid
    {

        [JsonProperty("uri")]
        public string uri { get; set; }

        [JsonProperty("uriPath")]
        public string uriPath { get; set; }

        [JsonProperty("path")]
        public object path { get; set; }

        [JsonProperty("host")]
        public string host { get; set; }
    }

    public class SourceClientId
    {

        [JsonProperty("uri")]
        public string uri { get; set; }

        [JsonProperty("uriPath")]
        public string uriPath { get; set; }

        [JsonProperty("path")]
        public object path { get; set; }

        [JsonProperty("host")]
        public string host { get; set; }
    }

    public class SourceName
    {

        [JsonProperty("content")]
        public string content { get; set; }
    }

    public class Source
    {

        [JsonProperty("sourceOrcid")]
        public SourceOrcid sourceOrcid { get; set; }

        [JsonProperty("sourceClientId")]
        public SourceClientId sourceClientId { get; set; }

        [JsonProperty("sourceName")]
        public SourceName sourceName { get; set; }
    }

    public class Subtitle
    {

        [JsonProperty("content")]
        public string content { get; set; }
    }

    public class TranslatedTitle
    {

        [JsonProperty("value")]
        public string value { get; set; }

        [JsonProperty("languageCode")]
        public string languageCode { get; set; }
    }

    public class Title
    {
        public Title(string title)
        {
            this.title = title;
        }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("subtitle")]
        public Subtitle subtitle { get; set; }

        [JsonProperty("translatedTitle")]
        public TranslatedTitle translatedTitle { get; set; }
    }

    public class JournalTitle
    {

        [JsonProperty("content")]
        public string content { get; set; }
    }

    public class Citation
    {

        [JsonProperty("citationType")]
        public string citationType { get; set; }

        [JsonProperty("citationValue")]
        public string citationValue { get; set; }
    }

    public class Year
    {
        public Year(string value)
        {
            this.value = value;
        }

        [JsonProperty("value")]
        public string value { get; set; }
    }

    public class Month
    {
        public Month(string value)
        {
            this.value = value;
        }

        [JsonProperty("value")]
        public string value { get; set; }
    }

    public class Day
    {
        public Day(string value)
        {
            this.value = value;
        }

        [JsonProperty("value")]
        public string value { get; set; }
    }

    public class PublicationDate
    {

        [JsonProperty("year")]
        public Year year { get; set; }

        [JsonProperty("month")]
        public Month month { get; set; }

        [JsonProperty("day")]
        public Day day { get; set; }

        public override string ToString()
        {
            if (day is null)
            {
                if (month is null) return year.value + "/01/01";
                else return year.value + "/" + month.value + "/01";
            }
            return year.value + "/" + month.value + "/" + day.value;
        }
    }

    public class ExternalId
    {
        public ExternalId(string externalIdType, string externalIdValue, string externalIdRelationship)
        {
            this.externalIdType = externalIdType;
            this.externalIdValue = externalIdValue;
            this.externalIdRelationship = externalIdRelationship;
        }

        [JsonProperty("createdDate")]
        public object createdDate { get; set; }

        [JsonProperty("lastModifiedDate")]
        public object lastModifiedDate { get; set; }

        [JsonProperty("source")]
        public object source { get; set; }

        [JsonProperty("putCode")]
        public object putCode { get; set; }

        [JsonProperty("visibility")]
        public object visibility { get; set; }

        [JsonProperty("displayIndex")]
        public object displayIndex { get; set; }

        [JsonProperty("path")]
        public object path { get; set; }

        [JsonProperty("externalIdType")]
        public string externalIdType { get; set; }

        [JsonProperty("externalIdValue")]
        public string externalIdValue { get; set; }

        [JsonProperty("externalIdUrl")]
        public string externalIdUrl { get; set; }

        [JsonProperty("externalIdRelationship")]
        public string externalIdRelationship { get; set; }
    }

    public class ExternalIds
    {
        public ExternalIds(List<ExternalId> externalId)
        {
            this.externalId = externalId;
        }

        [JsonProperty("externalId")]
        public List<ExternalId> externalId { get; set; }
    }

    public class Url
    {
        public Url(string value)
        {
            this.value = value;
        }

        [JsonProperty("value")]
        public string value { get; set; }
    }

    public class ContributorOrcid
    {

        [JsonProperty("uri")]
        public string uri { get; set; }

        [JsonProperty("uriPath")]
        public string uriPath { get; set; }

        [JsonProperty("path")]
        public string path { get; set; }

        [JsonProperty("host")]
        public string host { get; set; }
    }

    public class CreditName
    {
        public CreditName(string value)
        {
            this.value = value;
        }

        [JsonProperty("value")]
        public string value { get; set; }
    }

    public class ContributorEmail
    {

        [JsonProperty("value")]
        public string value { get; set; }
    }

    public class ContributorAttributes
    {
        public ContributorAttributes(string contributorRole)
        {
            foreach (string role in Enum.GetNames(typeof(ContributorRole)))
            {
                if (contributorRole.ToUpper().Equals(role))
                    this.contributorRole = role;
            }
        }

        [JsonProperty("contributorSequence")]
        public string contributorSequence { get; set; }

        [JsonProperty("contributorRole")]
        public string contributorRole { get; set; }
    }

    public class Contributor
    {
        public Contributor(CreditName creditName, ContributorAttributes contributorAttributes)
        {
            this.creditName = creditName;
            this.contributorAttributes = contributorAttributes;
        }

        [JsonProperty("contributorOrcid")]
        public ContributorOrcid contributorOrcid { get; set; }

        [JsonProperty("creditName")]
        public CreditName creditName { get; set; }

        [JsonProperty("contributorEmail")]
        public ContributorEmail contributorEmail { get; set; }

        [JsonProperty("contributorAttributes")]
        public ContributorAttributes contributorAttributes { get; set; }
    }

    public class Contributors
    {
        public Contributors(List<Contributor> contributor)
        {
            this.contributor = contributor;
        }

        [JsonProperty("contributor")]
        public List<Contributor> contributor { get; set; }

        public string ToString(string year, string sourceName)
        {
            var ListToReturn = new List<string>();
            foreach (var item in contributor)
            {
                ListToReturn.Add(item.creditName.value);
            }
            return "Contributors: " + string.Join("; ", ListToReturn) + Environment.NewLine + " || Year: " + year + Environment.NewLine + " || SourceName: " + sourceName;
        }
    }

    public class Country
    {

        [JsonProperty("value")]
        public string value { get; set; }

        [JsonProperty("visibility")]
        public string visibility { get; set; }
    }

    public class Work
    {

        [JsonProperty("createdDate")]
        public CreatedDate createdDate { get; set; }

        [JsonProperty("lastModifiedDate")]
        public LastModifiedDate lastModifiedDate { get; set; }

        [JsonProperty("source")]
        public Source source { get; set; }

        [JsonProperty("putCode")]
        public object putCode { get; set; }

        [JsonProperty("visibility")]
        public string visibility { get; set; }

        [JsonProperty("displayIndex")]
        public object displayIndex { get; set; }

        [JsonProperty("path")]
        public string path { get; set; }

        [JsonProperty("title")]
        public Title title { get; set; }

        [JsonProperty("journalTitle")]
        public JournalTitle journalTitle { get; set; }

        [JsonProperty("shortDescription")]
        public string shortDescription { get; set; }

        [JsonProperty("citation")]
        public Citation citation { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("publicationDate")]
        public PublicationDate publicationDate { get; set; }

        [JsonProperty("externalIds")]
        public ExternalIds externalIds { get; set; }

        [JsonProperty("url")]
        public Url url { get; set; }

        [JsonProperty("contributors")]
        public Contributors contributors { get; set; }

        [JsonProperty("languageCode")]
        public string languageCode { get; set; }

        [JsonProperty("country")]
        public Country country { get; set; }
    }



    public enum CitationType { FORMATTED_UNSPECIFIED, BIBTEX, FORMATTED_APA, FORMATTED_HARVARD, FORMATTED_IEEE, FORMATTED_MLA, FORMATTED_VANCOUVER, FORMATTED_CHICAGO, RIS };

    public enum ExternalIdRelationship { SELF, PART_OF };

    public enum ExternalIdType { Doi, Eid, Handle, Issn, OtherId, SourceWorkId };

    public enum Host { OrcidOrg };

    public enum TypeEnum { ARTISTIC_PERFORMANCE, BOOK_CHAPTER, BOOK_REVIEW, BOOK, CONFERENCE_ABSTRACT, CONFERENCE_PAPER, CONFERENCE_POSTER, DATA_SET, DICTIONARY_ENTRY, DISCLOSURE, DISSERTATION, EDITED_BOOK, ENCYCLOPEDIA_ENTRY, INVENTION, JOURNAL_ARTICLE, JOURNAL_ISSUE, LECTURE_SPEECH, LICENSE, MAGAZINE_ARTICLE, MANUAL, NEWSLETTER_ARTICLE, NEWSPAPER_ARTICLE, ONLINE_RESOURCE, OTHER, PATENT, REGISTERED_COPYRIGHT, REPORT, RESEARCH_TECHNIQUE, RESEARCH_TOOL, SPIN_OFF_COMPANY, STANDARDS_AND_POLICY, SUPERVISED_STUDENT_PUBLICATION, TECHNICAL_STANDARD, TEST, TRADEMARK, TRANSLATION, WEBSITE, WORKING_PAPER, UNDEFINED };

    public enum Visibility { LIMITED, REGISTERED_ONLY, PUBLIC };

    public enum CountryType { AF, AX, AL, DZ, AS, AD, AO, AI, AQ, AG, AR, AM, AW, AU, AT, AZ, BS, BH, BD, BB, BY, BE, BZ, BJ, BM, BT, BO, BQ, BA, BW, BV, BR, IO, BN, BG, BF, BI, KH, CM, CA, CV, KY, CF, TD, CL, CN, CX, CC, CO, KM, CG, CD, CK, CR, CI, HR, CU, CW, CY, CZ, DK, DJ, DM, DO, EC, EG, SV, GQ, ER, EE, ET, FK, FO, FJ, FI, FR, GF, PF, TF, GA, GM, GE, DE, GH, GI, GR, GL, GD, GP, GU, GT, GG, GN, GW, GY, HT, HM, VA, HN, HK, HU, IS, IN, ID, IR, IQ, IE, IM, IL, IT, JM, JP, JE, JO, KZ, KE, KI, KP, KR, KW, KG, LA, LV, LB, LS, LR, LY, LI, LT, LU, MO, MK, MG, MW, MY, MV, ML, MT, MH, MQ, MR, MU, YT, MX, FM, MD, MC, MN, ME, MS, MA, MZ, MM, NA, NR, NP, NL, NC, NZ, NI, NE, NG, NU, NF, MP, NO, OM, PK, PW, PS, PA, PG, PY, PE, PH, PN, PL, PT, PR, QA, RE, RO, RU, RW, BL, SH, KN, LC, MF, PM, VC, WS, SM, ST, SA, SN, RS, SC, SL, SG, SX, SK, SI, SB, SO, ZA, GS, SS, ES, LK, SD, SR, SJ, SZ, SE, CH, SY, TJ, TZ, TH, TL, TG, TK, TO, TT, TN, TR, TM, TC, TV, UG, UA, AE, GB, US, UM, UY, UZ, VU, VE, VN, VG, VI, WF, EH, YE, ZM, ZW, TW, XK }

    public enum ContributorRole { AUTHOR, ASSIGNEE, EDITOR, CHAIR_OR_TRANSLATOR, CO_INVESTIGATOR, CO_INVENTOR, GRADUATE_STUDENT, OTHER_INVENTOR, PRINCIPAL_INVESTIGATOR, POSTDOCTORAL_RESEARCHER, SUPPORT_STAFF };

    public enum ContributorSequence { FIRST, ADDITIONAL };
}