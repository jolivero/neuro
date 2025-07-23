namespace Neuro.AI.Graph.Models.Dtos
{
    public class DailyTaskDto
    {
        public string MonthId { get; set; } = string.Empty;
        public string TurnId { get; set; } = string.Empty;
        public List<AssigmentsDto> Assigments { get; set; } = [];
    }

    public class AssigmentsDto
    {
        public string DayId { get; set; } = string.Empty;
        public string BeginAt { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string StationId { get; set; } = string.Empty;
        public string MachineId { get; set; } = string.Empty;
    }
}