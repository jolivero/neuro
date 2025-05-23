using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Machine
{
    public Guid MachineId { get; set; }

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public decimal? EnergyConsumption { get; set; }

    public int? MaintenancePeriod { get; set; }

    public int? Velocity { get; set; }

    public int? MinOperator { get; set; }

    public int? MaxOperator { get; set; }

    public int? HoursPerCut { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public Guid? StationId { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetails { get; set; } = new List<ChangeRequestDetail>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<DailyTask> DailyTasks { get; set; } = new List<DailyTask>();

    public virtual ICollection<MachineReport> MachineReports { get; set; } = new List<MachineReport>();

    public virtual Station? Station { get; set; }
}
