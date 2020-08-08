using Newtonsoft.Json;
namespace SmartConnectors.Components.Salesforce.Model.Json
{
    public class Location
    {
        [JsonProperty(PropertyName = "latitude")]
        public string Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public string Longitude { get; set; }
    }
}
