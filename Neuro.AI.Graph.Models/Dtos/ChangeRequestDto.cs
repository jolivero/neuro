namespace Neuro.AI.Graph.Models.Dtos
{
    public class BaseChangeRequestDto
    {
        public string RequestingUserId { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }

    public class FieldsChangeRequetsDto : BaseChangeRequestDto
    {
        public string LineId { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string StationId { get; set; } = string.Empty;
        public string MachineId { get; set; } = string.Empty;
        public string PartId { get; set; } = string.Empty;
        
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

    public class ChangePlanificationRequestDto : FieldsChangeRequetsDto
    {
        public string TaskId { get; set; } = string.Empty;
        public string BeginAt { get; set; } = string.Empty;
        public string EndAt { get; set; } = string.Empty;
        public int NewValue { get; set; }
    }

    public class SpecialMissionRequestDto : BaseChangeRequestDto
    {
        public string TaskId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }

    public class ExtraTimeRequestDto : FieldsChangeRequetsDto
    {
        public decimal HoursQuantity { get; set; }
        public List<string> UserIds { get; set; } = [];
    }

    public class ChangeRequestDto
    {
        public string? TaskId { get; set; }
        public string? NcPartId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public string OriginRequest { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
        public string? Reason { get; set; }
        public string? CurrentValue { get; set; }
        public string? NewValue { get; set; }
    }

    public class UpdateStatusRequestDto
    {
        public string RequestId { get; set; } = string.Empty;
        public string ApprovalUserId { get; set; } = string.Empty;
        public string? Response { get; set; }
        public bool Status { get; set; }
    }
}