namespace Neuro.AI.Graph.Models.Dtos
{
    public class MonthlyPlanningDto
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int MonthlyGoal { get; set; }
        public int BusinessDays { get; set; }
        public int ExtraDays { get; set; }
        public string LineId { get; set; } = string.Empty;
        public string PlannedBy { get; set; } = string.Empty;
        public List<DailyPlanningDto> DailyPlanning { get; set; } = [];
    }

    public class DailyPlanningDto
    {
        public int DailyGoal { get; set; }
        public string ProductionDate { get; set; } = string.Empty;
        public string DayType { get; set; } = string.Empty;
    }

    public class UpdateMonthlyPlanningDto
    {
        public string MonthId { get; set; } = string.Empty;
        public int MonthlyGoal { get; set; }
        public string Reason { get; set; } = string.Empty;
        public List<UpdateDailyPlanningDto>? UpdateDailyPlanningDto { get; set; } = [];
    }

    public class UpdateDailyPlanningDto : DailyPlanningDto
    {
        public string? DayId { get; set; }
        public int Available { get; set; }
    }
}