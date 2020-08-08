namespace SmartConnectors.Models
{
    public class Connector: BaseEntity
    {
        public string Name { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyWebsite { get; set; }
        public bool IsPrimary { get; set; }
    }
}
