using System.Xml.Serialization;
namespace SmartConnectors.Components.Salesforce.Model.Xml
{
    
    [XmlRoot(Namespace = "http://soap.sforce.com/2006/04/metadata", ElementName = "result", IsNullable = true)]
    public class CreateResult
    {
        [XmlElement(ElementName = "done")]
        public bool Done;

        [XmlElement(ElementName = "id")]
        public string Id;

        [XmlElement(ElementName = "state")]
        public string State;
    }
}
