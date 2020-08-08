namespace SmartConnectors.Models
{
    public class Credential: BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string SecretToken { get; set; }

        public string HostUrl { get; set; }

        public string RememberMe { get; set; }

        public string OrgId { get; set; }

        public string EndPointName { get; set; }

        public int ConnectorId { get; set; }
        public int WorkflowId { get; set; }
    }
}
