using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ChangeRequestDetail
{
    public Guid DetailId { get; set; }

    public string RequestType { get; set; } = null!;

    public string? Description { get; set; }

    public int? HoursQuantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? RequestId { get; set; }

    public Guid? StationId { get; set; }

    public Guid? MachineId { get; set; }

    public Guid? PartId { get; set; }

    public string? CurrentValue { get; set; }

    public string? NewValue { get; set; }

    public Guid? CurrentUserId { get; set; }

    public Guid? NewUserId { get; set; }

    public Guid? CurrentStationId { get; set; }

    public Guid? CurrentMachineId { get; set; }

    public Guid? CurrentTurnId { get; set; }

    public Guid? NewTurnId { get; set; }

    public virtual Machine? CurrentMachine { get; set; }

    public virtual Station? CurrentStation { get; set; }

    public virtual Turn? CurrentTurn { get; set; }

    public virtual User? CurrentUser { get; set; }

    public virtual Machine? Machine { get; set; }

    public virtual Turn? NewTurn { get; set; }

    public virtual User? NewUser { get; set; }

    public virtual Part? Part { get; set; }

    public virtual ProductionChangeRequest? Request { get; set; }

    public virtual Station? Station { get; set; }
}
