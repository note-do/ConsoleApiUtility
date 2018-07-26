using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiUtility.Domain.Models.ValueObjects
{

    [JsonObject("Attributes", ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Attribute
    {
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
#warning Отстутствует типизация
        [JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
        [JsonProperty("Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
#warning Неочевидные типы атрибутов. Лучше внедрить Enum
        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public int AttributeType { get; set; }
    }
}
