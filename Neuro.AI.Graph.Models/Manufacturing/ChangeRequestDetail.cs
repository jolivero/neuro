using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ChangeRequestDetail
{
    public int DetailId { get; set; }

    public string RequestType { get; set; } = null!;

    public string? Description { get; set; }

    public string? CurrentValue { get; set; }

    public string? NewValue { get; set; }

    public int? HoursQuantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? RequestId { get; set; }

    public int? CurrentLineId { get; set; }

    public int? NewLineId { get; set; }

    public int? CurrentGroupId { get; set; }

    public int? NewGroupId { get; set; }

    public int? CurrentStationId { get; set; }

    public int? NewStationId { get; set; }

    public int? CurrentMachineId { get; set; }

    public int? NewMachineId { get; set; }

    public int? CurrentUserId { get; set; }

    public int? NewUserId { get; set; }

    public int? CurrentTurnId { get; set; }

    public int? NewTurnId { get; set; }

    public string? CurrentTime { get; set; }

    public string? NewTime { get; set; }

    public int? CurrentPartId { get; set; }

    public int? NewPartId { get; set; }

    public virtual Group? CurrentGroup { get; set; }

    public virtual ProductionLine? CurrentLine { get; set; }

    public virtual Machine? CurrentMachine { get; set; }

    public virtual Part? CurrentPart { get; set; }

    public virtual Station? CurrentStation { get; set; }

    public virtual Turn? CurrentTurn { get; set; }

    public virtual User? CurrentUser { get; set; }

    public virtual Group? NewGroup { get; set; }

    public virtual ProductionLine? NewLine { get; set; }

    public virtual Machine? NewMachine { get; set; }

    public virtual Part? NewPart { get; set; }

    public virtual Station? NewStation { get; set; }

    public virtual Turn? NewTurn { get; set; }

    public virtual User? NewUser { get; set; }

    public virtual ProductionChangeRequest? Request { get; set; }
}
