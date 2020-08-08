using Newtonsoft.Json.Serialization;
using System;
namespace SmartConnectors.Components.Salesforce.Serializer
{
    public class CreateableContractResolver: IContractResolver
    {
        public CreateableContractResolver()
        {
        }

        public JsonContract ResolveContract(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
