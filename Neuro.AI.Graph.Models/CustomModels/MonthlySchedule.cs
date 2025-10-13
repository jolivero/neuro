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
        public string Status { get; set; } = string.Empty;
        public TimeOnly BeginTime { get; set; }
        public TimeOnly EndTime { get; set; }
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
        public int LineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MonthlyGoal { get; set; }
        public int CurrentProgress { get; set; }
        public decimal Progress { get; set; }
    }

    public class MonthlyPlanningStepStatus
    {
        public int? MonthId { get; set; }
        public int StationId { get; set; }
        public int MachineId { get; set; }
        public int Status { get; set; }
    }
    
}