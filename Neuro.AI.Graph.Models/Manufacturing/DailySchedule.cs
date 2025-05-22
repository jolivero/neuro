using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class DailySchedule
{
    public Guid DayId { get; set; }

    public int DailyGoal { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? MonthId { get; set; }
}
