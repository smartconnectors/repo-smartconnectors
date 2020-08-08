using System.Collections.Generic;

namespace SmartConnectors.Models
{
    public class Workflow : BaseEntity
    {
        public string Name { get; set; }      
        public int ProjectId { get; set; }
        public string PackageName { get; set; }

        public List<Workflow> Children { get; set; }

        public Workflow()
        {
            this.Children = new List<Workflow>();
        }
    }
}
