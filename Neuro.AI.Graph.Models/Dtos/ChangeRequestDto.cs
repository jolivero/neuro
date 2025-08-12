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
        public string StationId { get; set; } = string.Empty;
        public string MachineId { get; set; } = string.Empty;
    }

    public class UpdateStatusRequestDto
    {
        public string RequestId { get; set; } = string.Empty;
        public string ApprovalUserId { get; set; } = string.Empty;
        public string? Response { get; set; }
        public bool Status { get; set; }
    }
}