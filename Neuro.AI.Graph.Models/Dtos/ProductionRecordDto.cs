namespace Neuro.AI.Graph.Models.Dtos
{
    public class ProductionRecordDto
    {
        public int TaskId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CurrentTime { get; set; } = string.Empty;
        public bool IsCut { get; set; } = false;
        public ProducedPart? ProducedPart { get; set; }
        public List<ProducedPart>? NcProducedPart { get; set; }
    }

    public class ProducedPart
    {
        public int? Total { get; set; }
        public int? PartId { get; set; }
    }
}