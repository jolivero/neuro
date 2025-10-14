namespace Neuro.AI.Graph.Models.Dtos
{
    public class QualityRecordDto
    {
        public int NcPartId { get; set; }
        public Guid CreatedBy { get; set; }
        public QualityClasificationDto QualityClasificationDto { get; set; } = new();
    }

    public class QualityClasificationDto
    {
        public int MinParts { get; set; }
        public int Reprocess { get; set; }
        public List<ScrapDto> ScrapDto { get; set; } = [];
    }

    public class ScrapDto
    {
        public int Scrap { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}