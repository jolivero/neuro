using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ProductionLine
{
    public Guid LineId { get; set; }

    public string Name { get; set; } = null!;

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CompanyId { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual Company? Company { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<MonthlySchedule> MonthlySchedules { get; set; } = new List<MonthlySchedule>();
}
