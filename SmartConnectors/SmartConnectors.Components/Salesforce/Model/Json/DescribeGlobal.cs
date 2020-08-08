using System.Collections.Generic;
using Newtonsoft.Json;

namespace SmartConnectors.Components.Salesforce.Model.Json
{
    public class DescribeGlobal
    {
        [JsonProperty(PropertyName = "encoding")]
        public string Encoding { get; set; }

        [JsonProperty(PropertyName = "maxBatchSize")]
        public int MaxBatchSize { get; set; }

        [JsonProperty(PropertyName = "sobjects")]
        public List<SObjectDescribeBasic> SObjects { get; set; }
    }
}
