namespace Neuro.AI.Graph.Models.Dtos
{
    public class MonthlyScheduleDto
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int MonthlyGoal { get; set; }
        public int BusinessDays { get; set; }
        public int ExtraDays { get; set; }
        public string LineId { get; set; } = string.Empty;
        public string? TurnId { get; set; }
        public string PlannedBy { get; set; } = string.Empty;
        public List<DailyScheduleDto> DailySchedule { get; set; } = [];
    }

    public class DailyScheduleDto
    {
        public int DailyGoal { get; set; }
        public string ProductionDate { get; set; } = string.Empty;
        public string DayType { get; set; } = string.Empty;
    }

    public class UpdateMonthlyScheduleDto
    {
        public string MonthId { get; set; } = string.Empty;
        public int MonthlyGoal { get; set; }
        public string Reason { get; set; } = string.Empty;
        public List<UpdateDailyScheduleDto>? UpdateDailyScheduleDto { get; set; } = [];
    }

    public class UpdateDailyScheduleDto : DailyScheduleDto
    {
        public string? DayId { get; set; }
        public int Available { get; set; }
    }
}