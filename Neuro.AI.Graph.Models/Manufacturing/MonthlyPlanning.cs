using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class MonthlyPlanning
{
    public int MonthId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public int MonthlyGoal { get; set; }

    public int BusinessDays { get; set; }

    public int ExtraDays { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Available { get; set; }

    public string? Reason { get; set; }

    public int? LineId { get; set; }

    public Guid? PlannedBy { get; set; }

    public virtual ICollection<DailyPlanning> DailyPlannings { get; set; } = new List<DailyPlanning>();

    public virtual ProductionLine? Line { get; set; }

    public virtual User? PlannedByNavigation { get; set; }

    public virtual ICollection<ProductionChangeRequest> ProductionChangeRequests { get; set; } = new List<ProductionChangeRequest>();
}
