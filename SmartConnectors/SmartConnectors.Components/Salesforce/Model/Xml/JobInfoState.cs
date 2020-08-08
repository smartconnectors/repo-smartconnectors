using System.Xml.Serialization;

namespace SmartConnectors.Components.Salesforce.Model.Xml
{
    [XmlRoot(Namespace = "http://www.force.com/2009/06/asyncapi/dataload",
     ElementName = "jobInfo",
     IsNullable = false)]
    public class JobInfoState
    {
        [XmlElement(ElementName = "state")]
        public string State { get; set; }
    }
}
