namespace Neuro.AI.Graph.Models.Dtos
{
    public class BaseChangeRequestDto
    {
        public string RequestingUserId { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }

    public class MonthlyChangeRequestDto : BaseChangeRequestDto
    {
        public string MonthId { get; set; } = string.Empty;
        public int CurrentValue { get; set; }
        public int NewValue { get; set; }
    }

    public class DailyChangeRequestDto : BaseChangeRequestDto
    {
        public List<string> DayId { get; set; } = [];
        public string CurrentValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
        public string CurrentUserId { get; set; } = string.Empty;
        public string? NewUserId { get; set; }
        public string CurrentStationId { get; set; }  = string.Empty;
        public string? StationId { get; set; }
        public string CurrentMachineId { get; set; } = string.Empty;
        public string? MachineId { get; set; }
        public string? CurrentTurnId { get; set; }
        public string? NewTurnId { get; set; }
    }

    public class UpdateStatusRequestDto
    {
        public string RequestId { get; set; } = string.Empty;
        public string ApprovalUserId { get; set; } = string.Empty;
        public string? Response { get; set; }
        public bool Status { get; set; }
    }
}