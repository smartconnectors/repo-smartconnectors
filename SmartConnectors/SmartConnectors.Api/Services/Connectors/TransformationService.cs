using SmartConnectors.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace SmartConnectors.Api.Services.Connectors
{
    public class TransformationService : OperationLogService
    {
        private readonly string _conStr;

        public TransformationService(string conStr, int workflowId) : base(conStr, workflowId)
        {
            _conStr = conStr;
        }

        public async Task<List<IDictionary<string, object>>> ExecuteOperation(ActiveOperation ops, List<Dictionary<string, object>> records)
        {
            // TODO
            // 1) Read file content from blob storage  
            // 2> Return List from excel data
            List<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
            List<string> columns = new List<string>();

            dynamic payload;
            dynamic script;

            if (ops != null)
            {
                payload = JsonConvert.DeserializeObject(ops.Content);
               // script = JsonConvert.DeserializeObject(ops.Script);

                foreach (var property in payload)
                {
                    foreach (var property1 in property)
                    {
                        columns.Add(property1.Name.ToLower());
                    }
                }
            }

            foreach (var item in records)
            {
                IDictionary<string, object> objectFields = new ExpandoObject();

                // Iterate through fields and populate dynamic object.
                foreach (KeyValuePair<string, object> f in item)
                {
                    if (columns.Contains(f.Key.ToLower()))
                    {
                        objectFields.Add(f.Key, f.Value);
                    }
                }

                list.Add(objectFields);
            }

            return list;
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        public class TransformData
        {
            public object Payload { get; set; }
        }
    }
}
