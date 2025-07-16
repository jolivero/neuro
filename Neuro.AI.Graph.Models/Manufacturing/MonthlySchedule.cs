using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class MonthlySchedule
{
    public Guid MonthId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public int MonthlyGoal { get; set; }

    public int BusinessDays { get; set; }

    public int ExtraDays { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Available { get; set; }

    public Guid? LineId { get; set; }

    public Guid? TurnId { get; set; }

    public Guid? PlannedBy { get; set; }

    public string? Reason { get; set; }

    public virtual ICollection<DailySchedule> DailySchedules { get; set; } = new List<DailySchedule>();

    public virtual ProductionLine? Line { get; set; }

    public virtual User? PlannedByNavigation { get; set; }
}
