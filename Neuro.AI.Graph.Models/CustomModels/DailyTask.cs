using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Models.CustomModels
{
    public class DailyTaskOperator : DailySchedule
    {
        public Guid TaskId { get; set; } = new();
        public string OperatorStatus { get; set; } = string.Empty;
        public Progress Progress { get; set; } = new();
        public User User { get; set; } = new();
        public DailyTaskStation Station { get; set; } = new();
    }

    public class DailyTaskStation
    {
        public Guid StationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Machine Machine { get; set; } = new();
        public Part Part { get; set; } = new();
        public List<Part> PrevPart { get; set; } = [];
    }

    public class Progress
    {
        public int Total { get; set; }
        public decimal Compliance { get; set; }
    }
    
}