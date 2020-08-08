using Newtonsoft.Json;
using SmartConnectors.Components.Salesforce.Model.Json;

namespace SmartConnectors.Components.Salesforce.Common
{
    /// <summary>
    /// Interface enforcing implementation of Attributes Property for multiple record updates
    /// </summary>
    public interface IAttributedObject
    {
        [JsonProperty(PropertyName = "attributes")]
        SObjectAttributes Attributes { get; set; }
    }
}
