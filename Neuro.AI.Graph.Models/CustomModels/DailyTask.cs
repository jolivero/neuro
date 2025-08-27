using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Models.CustomModels
{
    public class DailyTaskOperator : DailySchedule
    {
        public Guid TaskId { get; set; }
        public string OperatorStatus { get; set; } = string.Empty;
        public Compliance Compliance { get; set; } = new();
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

    public class Compliance
    {
        public int Total { get; set; }
        public decimal Progress { get; set; }
    }
    
}