namespace SmartConnectors.Models
{
    public class WorkflowConnector : BaseEntity
    {
        public int Pos { get; set; }
        public int ConnectorId { get; set; }
        public int WorkflowId { get; set; }
        public bool IsTransformationEnabled { get; set; }
        public bool IsPreScriptingEnabled { get; set; }
        public bool IsPostScriptingEnabled { get; set; }
    }
}
