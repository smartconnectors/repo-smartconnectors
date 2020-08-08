namespace SmartConnectors.Models
{
    public class Document : BaseEntity
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string Type { get; set; }
        public byte[] Content { get; set; }
        public string Description { get; set; }
        public bool IsProcessed { get; set; }
        public string FullPath { get; set; }
        public string WebUrl { get; set; }
        public int WorkflowId { get; set; }

    }
}
