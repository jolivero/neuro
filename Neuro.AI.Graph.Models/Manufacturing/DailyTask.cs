using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class DailyTask
{
    public Guid TaskId { get; set; }

    public string? CustomeBeginAt { get; set; }

    public string? CustomeEndAt { get; set; }

    public string? OperatorStatus { get; set; }

    public string? MachineStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? DayId { get; set; }

    public Guid? StationId { get; set; }

    public Guid? MachineId { get; set; }

    public Guid? UserId { get; set; }

    public virtual DailySchedule? Day { get; set; }

    public virtual Machine? Machine { get; set; }

    public virtual ICollection<ProductionRecord> ProductionRecords { get; set; } = new List<ProductionRecord>();

    public virtual Station? Station { get; set; }

    public virtual User? User { get; set; }
}
