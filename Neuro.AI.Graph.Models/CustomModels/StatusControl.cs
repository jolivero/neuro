namespace Neuro.AI.Graph.Models.CustomModels
{
    public class StatusControl
    {
        public Guid TaskId { get; set; }
        public string Station { get; set; } = string.Empty;
        public Guid OperatorId { get; set; }
        public string Operator { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string ProductiveTime { get; set; } = string.Empty;
        public string PauseTime { get; set; } = string.Empty;
        public string TotalTime { get; set; } = string.Empty;
        public int DailyGoal { get; set; }
        public int TotalProduced { get; set; }
        public decimal Progress { get; set; }
    }
}