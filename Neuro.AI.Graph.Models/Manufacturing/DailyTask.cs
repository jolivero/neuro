using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class DailyTask
{
    public int TaskId { get; set; }

    public TimeOnly? BeginAt { get; set; }

    public TimeOnly? EndAt { get; set; }

    public string? OperatorStatus { get; set; }

    public string? MachineStatus { get; set; }

    public int? TaskGoal { get; set; }

    public int? Available { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DayId { get; set; }

    public int? StationId { get; set; }

    public int? MachineId { get; set; }

    public int? UserId { get; set; }

    public int? TurnId { get; set; }

    public virtual DailyPlanning? Day { get; set; }

    public virtual Machine? Machine { get; set; }

    public virtual ICollection<NonCompliantPartsRecord> NonCompliantPartsRecords { get; set; } = new List<NonCompliantPartsRecord>();

    public virtual ICollection<ProducedPartsRecord> ProducedPartsRecords { get; set; } = new List<ProducedPartsRecord>();

    public virtual ICollection<ProductionChangeRequest> ProductionChangeRequests { get; set; } = new List<ProductionChangeRequest>();

    public virtual ICollection<ProductionRecord> ProductionRecords { get; set; } = new List<ProductionRecord>();

    public virtual Station? Station { get; set; }

    public virtual Turn? Turn { get; set; }

    public virtual User? User { get; set; }
}
