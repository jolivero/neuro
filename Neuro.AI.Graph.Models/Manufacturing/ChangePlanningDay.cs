using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ChangePlanningDay
{
    public int ChangeDayId { get; set; }

    public int? MonthId { get; set; }

    public int? LineId { get; set; }

    public int? DayId { get; set; }

    public int? DailyGoal { get; set; }

    public DateOnly? ProductionDate { get; set; }

    public string? DayType { get; set; }

    public int? Available { get; set; }

    public int? RequestId { get; set; }

    public virtual ProductionChangeRequest? Request { get; set; }
}
