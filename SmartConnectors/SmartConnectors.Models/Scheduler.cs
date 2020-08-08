using System;

namespace SmartConnectors.Models
{
    public class Scheduler : BaseEntity
    {
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public bool IsRepeated { get; set; }

        public int RepeatOptionId { get;set;}

        public int WorkflowId { get; set; }
    }
}
