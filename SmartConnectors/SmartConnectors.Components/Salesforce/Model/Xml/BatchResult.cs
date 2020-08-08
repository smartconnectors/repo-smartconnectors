using System.Xml.Serialization;

namespace SmartConnectors.Components.Salesforce.Model.Xml
{
    [XmlType(TypeName = "result")]
    public class BatchResult
    {
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "success")]
        public bool Success { get; set; }

        [XmlElement(ElementName = "created")]
        public bool Created { get; set; }

        [XmlElement(ElementName = "errors")]
        public BatchResultErrors Errors { get; set; }
    }
}
