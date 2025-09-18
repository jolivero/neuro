namespace Neuro.AI.Graph.Models.CustomModels
{
    public class DailyPlanningSummary
    {
        public Guid MonthId { get; set; }
        public int MonthlyGoal { get; set; }
        public int CurrentGoal { get; set; }
        public int ProductiveDays { get; set; }
        public int DailyGoal { get; set; }
    }

    public class DailyPlanningProductionLine
    {
        public Guid GroupId { get; set; }
        public string Group { get; set; } = string.Empty;
        public Guid StationId { get; set; }
        public string Station { get; set; } = string.Empty;
        public Guid MachineId { get; set; }
        public string Machine { get; set; } = string.Empty;
        public Guid TaskId { get; set; }
        public string MachineStatus { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string Operator { get; set; } = string.Empty;
        public string OperatorStatus { get; set; } = string.Empty;
        public string Schedule { get; set; } = string.Empty;
        public Guid DayId { get; set; }
        public int DailyGoal { get; set; }
        public int Inventory { get; set; }
    }
    
}