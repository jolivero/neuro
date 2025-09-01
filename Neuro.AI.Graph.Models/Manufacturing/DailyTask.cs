using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class DailyTask
{
    public Guid TaskId { get; set; }

    public TimeOnly? BeginAt { get; set; }

    public TimeOnly? EndAt { get; set; }

    public string? OperatorStatus { get; set; }

    public string? MachineStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? DayId { get; set; }

    public Guid? StationId { get; set; }

    public Guid? MachineId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? TurnId { get; set; }

    public virtual DailySchedule? Day { get; set; }

    public virtual Machine? Machine { get; set; }

    public virtual ICollection<NonCompliantPartsRecord> NonCompliantPartsRecords { get; set; } = new List<NonCompliantPartsRecord>();

    public virtual ICollection<ProducedPartsRecord> ProducedPartsRecords { get; set; } = new List<ProducedPartsRecord>();

    public virtual ICollection<ProductionChangeRequest> ProductionChangeRequests { get; set; } = new List<ProductionChangeRequest>();

    public virtual ICollection<ProductionRecord> ProductionRecords { get; set; } = new List<ProductionRecord>();

    public virtual Station? Station { get; set; }

    public virtual Turn? Turn { get; set; }

    public virtual User? User { get; set; }
}
