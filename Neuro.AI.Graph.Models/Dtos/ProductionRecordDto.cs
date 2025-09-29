namespace Neuro.AI.Graph.Models.Dtos
{
    public class ProductionRecordDto
    {
        public string TaskId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CurrentTime { get; set; } = string.Empty;
        public bool IsCut { get; set; } = false;
        public ProducedPart? ProducedPart { get; set; }
        public List<ProducedPart>? NcProducedPart { get; set; }
    }

    public class ProducedPart
    {
        public int? Total { get; set; }
        public string? PartId { get; set; }
    }
}