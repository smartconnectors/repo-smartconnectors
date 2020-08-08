namespace SmartConnectors.Components.Salesforce.ViewModels
{
    public class QueryRequest
    {
        public string ObjectId { get; set; }
        public string Token { get; set; }
        public string ApiVersion { get; set; }
        public string ClientName { get; set; }
        public string InstanceUrl { get; set; }

        public string ServerUri { get; set; }

        public string ObjectTypeName { get; set; }

        public object SfObject { get; set; }

        public string QueryString { get; set; }

        public bool QueryAll { get; set; }
    }

}
