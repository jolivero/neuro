using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class MonthlySchedule
{
    public Guid MonthId { get; set; }

    public string Month { get; set; } = null!;

    public int Year { get; set; }

    public int MonthlyGoal { get; set; }

    public int BusinessDays { get; set; }

    public int ExtraDays { get; set; }

    public Guid? LineId { get; set; }

    public Guid? TurnId { get; set; }

    public Guid? PlannedBy { get; set; }
}
