namespace Neuro.AI.Graph.Models.Dtos
{
    public class TurnDto
    {
        public string? TurnId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string ProductiveTime { get; set; } = string.Empty;
        public string PauseTime { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public List<TurnDetails> Details { get; set; } = [];
    }

    public class TurnDetails
    {
        public string? TurnDetailId { get; set; }
        public string PeriodType { get; set; } = string.Empty;
        public string BeginAt { get; set; } = string.Empty;
        public string EndAt { get; set; } = string.Empty;
        public string DurationDetail { get; set; } = string.Empty;
    }

}