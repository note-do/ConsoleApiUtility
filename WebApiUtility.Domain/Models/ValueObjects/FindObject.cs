using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace WebApiUtility.Domain.Models.ValueObjects
{
    public class FindObject
    {
        public Filter[] Filters { get; set; }
        public Condition[] Conditions { get; set; }
    }

    public class Filter
    {
        public int Type { get; set; }
        public string Value { get; set; }
    }

    public class Condition
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Direction { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Operator { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Logic { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Attribute { get; set; }
    }
 }
