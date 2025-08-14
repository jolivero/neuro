namespace Neuro.AI.Graph.Models.CustomModels
{
    public class TurnWithTimeDetail
    {
        public Guid TurnId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Duration { get; set; }
        public TimeDetail TimeDetail { get; set; } = new();
    }

    public class TimeDetail
    {
        public TimeOnly? BeginAt { get; set; }
        public TimeOnly? EndAt { get; set; }
    }

}