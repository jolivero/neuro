namespace Neuro.AI.Graph.Models.CustomModels
{
    public class DailyPlanningSummary
    {
        public int MonthId { get; set; }
        public int MonthlyGoal { get; set; }
        public int CurrentGoal { get; set; }
        public int ProductiveDays { get; set; }
        public int DailyGoal { get; set; }
    }

    public class DailyPlanningProductionLine
    {
        public int GroupId { get; set; }
        public string Group { get; set; } = string.Empty;
        public int StationId { get; set; }
        public string Station { get; set; } = string.Empty;
        public int MachineId { get; set; }
        public string Machine { get; set; } = string.Empty;
        public int PartId { get; set; }
        public string Part { get; set; } = string.Empty;
        public int TaskId { get; set; }
        public string MachineStatus { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string Operator { get; set; } = string.Empty;
        public string OperatorStatus { get; set; } = string.Empty;
        public TimeOnly LastUpdate { get; set; }
        public string Schedule { get; set; } = string.Empty;
        public int DayId { get; set; }
        public int DailyGoal { get; set; }
        public int Inventory { get; set; }
    }

}