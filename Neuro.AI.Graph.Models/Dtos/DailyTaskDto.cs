namespace Neuro.AI.Graph.Models.Dtos
{
    public class BaseDailyTaskDto
    {
        public int MonthId { get; set; }
        public int? TurnId { get; set; }
    }

    public class DailyTaskDto : BaseDailyTaskDto
    {
        public List<AssigmentsDto> Assigments { get; set; } = [];
    }

    public class AssigmentsDto
    {
        public int DayId { get; set; } 
        public string BeginAt { get; set; } = string.Empty;
        public string EndAt { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int? RemoveUserId { get; set; }
        public int StationId { get; set; }
        public int MachineId { get; set; }
    }

    public class CheckOperatorExtraTimeDto
    {
        public int UserId { get; set; }
        public string ProductiveDate { get; set; } = string.Empty;
        public int? TaskId { get; set; }
        public int? TurnId { get; set; }
        public string BeginAt { get; set; } = string.Empty;
        public string EndAt { get; set; } = string.Empty;
    }

}