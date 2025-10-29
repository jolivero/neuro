namespace Neuro.AI.Graph.Models.Dtos
{
    public class BaseChangeRequestDto
    {
        public Guid RequestingUserId { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }

    public class FieldsChangeRequetsDto : BaseChangeRequestDto
    {
        public int LineId { get; set; }
        public int GroupId { get; set; }
        public int StationId { get; set; }
        public int MachineId { get; set; }
        public int PartId { get; set; }
    }

    public class MonthlyChangeRequestDto : BaseChangeRequestDto
    {
        public int MonthId { get; set; }
        public int LineId { get; set; }
        public int CurrentValue { get; set; }
        public int NewValue { get; set; }
    }

    public class DailyChangeRequestDto : BaseChangeRequestDto
    {
        public List<int> DayId { get; set; } = [];
        public int LineId { get; set; }
        public string CurrentValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;
        public Guid? CurrentUserId { get; set; }
        public Guid? NewUserId { get; set; }
        public int CurrentStationId { get; set; }
        public int? StationId { get; set; }
        public int CurrentMachineId { get; set; }
        public int? MachineId { get; set; }
        public int? CurrentTurnId { get; set; }
        public int? NewTurnId { get; set; }
    }

    public class ChangePlanificationRequestDto : FieldsChangeRequetsDto
    {
        public int TaskId { get; set; }
        // public string BeginAt { get; set; } = string.Empty;
        // public string EndAt { get; set; } = string.Empty;
        // public int NewValue { get; set; }
    }

    public class CommonRequestDto : BaseChangeRequestDto
    {
        public int TaskId { get; set; }
        public Guid UserId { get; set; }
    }

    public class CommonChangeRequestDto : FieldsChangeRequetsDto
    {
        public int MonthId { get; set; }
        public int DayId { get; set; }
        public int TaskId { get; set; }
        public int? TurnId { get; set; }
        public Guid CurrentUserId { get; set; }
        public Guid? NewUserId { get; set; }
        public string CurrentValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;

    }

    public class ExtraTimeRequestDto : FieldsChangeRequetsDto
    {
        public string HoursQuantity { get; set; } = string.Empty;
        public List<Guid> UserIds { get; set; } = [];
    }

    public class ChangeRequestDto
    {
        public int? TaskId { get; set; }
        public int? NcPartId { get; set; }
        public Guid UserId { get; set; }
        public Guid CreatedBy { get; set; }
        public int CategoryId { get; set; }
        public string OriginRequest { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
        public string? Reason { get; set; }
        public string? CurrentValue { get; set; }
        public string? NewValue { get; set; }
    }

    public class ChangeRequestDtoV2 : CommonChangeRequestDto
    {
        public int CategoryId { get; set; }
        public string OriginRequest { get; set; } = string.Empty;
    }

    public class UpdateStatusRequestDto
    {
        public int RequestId { get; set; }
        public Guid ApprovalUserId { get; set; }
        public int CategoryId { get; set; }
        public int? TaskId { get; set; }
        public string? Response { get; set; }
        public bool Status { get; set; }
    }
}