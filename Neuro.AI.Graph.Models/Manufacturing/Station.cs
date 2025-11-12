using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Station
{
    public int StationId { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? GroupId { get; set; }

    public Guid? CreatedBy { get; set; }

    public int? BranchId { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetailCurrentStations { get; set; } = new List<ChangeRequestDetail>();

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetailNewStations { get; set; } = new List<ChangeRequestDetail>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<DailyTask> DailyTasks { get; set; } = new List<DailyTask>();

    public virtual Group? Group { get; set; }

    public virtual ICollection<MachineReport> MachineReports { get; set; } = new List<MachineReport>();

    public virtual ICollection<Part> Parts { get; set; } = new List<Part>();

    public virtual ICollection<ProductionLineRecipe> ProductionLineRecipes { get; set; } = new List<ProductionLineRecipe>();

    public virtual ICollection<Machine> Machines { get; set; } = new List<Machine>();
}
