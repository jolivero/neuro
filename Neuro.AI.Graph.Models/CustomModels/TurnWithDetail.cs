namespace Neuro.AI.Graph.Models.CustomModels
{
    public class TurnWithTimeDetail
    {
        public int TurnId { get; set; }
        public string Name { get; set; } = string.Empty;
        public TimeOnly? Duration { get; set; }
        public TimeOnly? ProductiveTime { get; set; }
        public TimeOnly? PauseTime { get; set; }
        public TimeDetail TimeDetail { get; set; } = new();
    }

    public class TimeDetail
    {
        public TimeOnly? BeginAt { get; set; }
        public TimeOnly? EndAt { get; set; }
    }

}