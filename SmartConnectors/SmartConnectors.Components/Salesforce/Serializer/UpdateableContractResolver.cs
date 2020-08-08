using Newtonsoft.Json.Serialization;
using System;
namespace SmartConnectors.Components.Salesforce.Serializer
{
    public class UpdateableContractResolver: IContractResolver
    {
        public UpdateableContractResolver()
        {
        }

        public JsonContract ResolveContract(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
