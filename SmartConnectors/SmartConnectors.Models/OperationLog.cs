using System;

namespace SmartConnectors.Models
{
    public class OperationLog : BaseEntity
    {
        public string Message { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public int WorkflowId { get; set; }
    }
}
