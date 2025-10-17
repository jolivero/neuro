using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Models.CustomModels
{
    public class OperatorSelectList
    {
        public Guid UserId { get; set; }
        public string Operator { get; set; } = string.Empty;
    }

    public class OperatorProfile : User
    {
        public List<OperatorSkills> OperatorSkills { get; set; } = [];
    }

    public class OperatorSkills
    {
        public int? SkillId { get; set; }
        public string? Name { get; set; }
        public string? SkillLevel { get; set; }
    }



    public class OperatorMonthlyPlanning : User
    {
        public List<OperatorDailyTasks> OperatorDailyTasks { get; set; } = [];
    }

    public class OperatorDailyTasks
    {
        public int TaskId { get; set; }
        public string? OperatorStatus { get; set; } = string.Empty;
        public TimeOnly? BeginAt { get; set; }
        public TimeOnly? EndAt { get; set; }
        public DailyPlanning Day { get; set; } = new();
        public ProductionLine ProductionLine { get; set; } = new();
        public Station Station { get; set; } = new();
        public Machine Machine { get; set; } = new();
        public OperatorCompliance OperatorCompliance { get; set; } = new();
    }

    public class OperatorCompliance
    {
        public int ProducedPartId { get; set; }
        public int Total { get; set; }
        public decimal Progress { get; set; }
        public int TaskId { get; set; }
    }
}