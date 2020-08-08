using System.Collections.Generic;
using System.Xml.Serialization;

namespace SmartConnectors.Components.Salesforce.Model.Xml
{
    public interface ISObjectList<T> : IList<T>, IXmlSerializable
    {
    }
}
