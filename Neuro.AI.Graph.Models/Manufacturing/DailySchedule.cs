using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class DailySchedule
{
    public Guid DayId { get; set; }

    public int DailyGoal { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Available { get; set; }

    public Guid? MonthId { get; set; }

    public string ProductionDate { get; set; } = null!;

    public string? DayType { get; set; }

    public string? Reason { get; set; }

    public virtual ICollection<DailyTask> DailyTasks { get; set; } = new List<DailyTask>();

    public virtual MonthlySchedule? Month { get; set; }
}
