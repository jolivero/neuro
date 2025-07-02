namespace Neuro.AI.Graph.Models.Dtos
{
    public class TurnDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Duration { get; set; }
        public decimal ProductiveTime { get; set; }
        public decimal PauseTime { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public List<TurnDetails> Details { get; set; } = [];
    }

    public class TurnDetails
    {
        public string? Id { get; set; }
        public string PeriodType { get; set; } = string.Empty;
        public string BeginAt { get; set; } = string.Empty;
        public string EndAt { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
    }

}