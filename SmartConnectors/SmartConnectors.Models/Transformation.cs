namespace SmartConnectors.Models
{
    public class Transformation : BaseEntity
    { 
        public string Input { get; set; }
        public string Output { get; set; }
        public string Script { get; set; }

        public string Payload { get; set; }

        public int WorkflowId { get; set; }
    }
}
