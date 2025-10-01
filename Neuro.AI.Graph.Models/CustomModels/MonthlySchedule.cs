using Neuro.AI.Graph.Models.Manufacturing;

namespace Neuro.AI.Graph.Models.CustomModels
{

    public class AnnualPlannigInfo
    {
        public int Year { get; set; }
        public int Days { get; set; }
        public int Tasks { get; set; }
        public int Operators { get; set; }
    }

    public class MonthlyPlanningProgress
    {
        public string Operator { get; set; } = string.Empty;
        public DateOnly ProductionDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Line { get; set; } = string.Empty;
        public int Total { get; set; }
        public decimal Progress { get; set; }
    }

    public class MonthlyPlanningProductionLines : MonthlyPlanning
    {
        public List<AssignedProductionLines> AssignedProductionLines { get; set; } = [];
    }

    public class AssignedProductionLines
    {
        public Guid LineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MonthlyGoal { get; set; }
        public int CurrentProgress { get; set; }
        public decimal Progress { get; set; }
    }

    public class MonthlyPlanningStepStatus
    {
        public string? MonthId { get; set; }
        public string StationId { get; set; } = string.Empty;
        public string MachineId { get; set; } = string.Empty;
        public int Status { get; set; }
    }
    
}