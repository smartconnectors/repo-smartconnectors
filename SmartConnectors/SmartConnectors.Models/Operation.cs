namespace SmartConnectors.Models
{
    public class Operation : BaseEntity
    {
        public int OperationTypeId { get; set; }
        public object Content { get; set; }
        public int StepCount { get; set; }
        public int WorkflowConnectorId { get; set; }
    }
}
