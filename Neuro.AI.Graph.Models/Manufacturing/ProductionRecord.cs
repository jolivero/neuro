using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ProductionRecord
{
    public int ProductionId { get; set; }

    public string Status { get; set; } = null!;

    public TimeOnly? BeginTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public TimeOnly? Duration { get; set; }

    public int? IsCut { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? TaskId { get; set; }

    public int? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual DailyTask? Task { get; set; }
}
