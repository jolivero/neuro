namespace Neuro.AI.Graph.Models.Dtos
{
    public class TurnDto
    {
        public int? TurnId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string ProductiveTime { get; set; } = string.Empty;
        public string PauseTime { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; } 
        public List<TurnDetails> Details { get; set; } = [];
    }

    public class TurnDetails
    {
        public int? Id { get; set; }
        public string PeriodType { get; set; } = string.Empty;
        public string BeginAt { get; set; } = string.Empty;
        public string EndAt { get; set; } = string.Empty;
        public string DurationDetail { get; set; } = string.Empty;
        public int? Available { get; set; }
    }

}