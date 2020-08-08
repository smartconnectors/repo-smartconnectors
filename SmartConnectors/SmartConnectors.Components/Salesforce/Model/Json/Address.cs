using Newtonsoft.Json;
namespace SmartConnectors.Components.Salesforce.Model.Json
{
    public class Address
    {
        [JsonProperty(PropertyName = "accuracy")]
        public string Accuracy { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public string Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public string Longitude { get; set; }

        [JsonProperty(PropertyName = "postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "stateCode")]
        public string StateCode { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

    }
}
