using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Station
{
    public Guid StationId { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? GroupId { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetails { get; set; } = new List<ChangeRequestDetail>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<DailyTask> DailyTasks { get; set; } = new List<DailyTask>();

    public virtual Group? Group { get; set; }

    public virtual ICollection<MachineReport> MachineReports { get; set; } = new List<MachineReport>();

    public virtual ICollection<Part> Parts { get; set; } = new List<Part>();

    public virtual ICollection<Machine> Machines { get; set; } = new List<Machine>();
}
