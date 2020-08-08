using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartConnectors.Components.Salesforce.Model.Json
{
    public class ErrorResult
    {
        [JsonProperty(PropertyName = "statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public List<string> Fields { get; set; }
    }
}
