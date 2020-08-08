namespace SmartConnectors.Components.Salesforce.ViewModels
{
    public class LoginRequest
    {
        public string EndpointName { get; set; }
        public string ServerHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string OrgId { get; set; }
        public string SecurityToken { get; set; }
    }
}

