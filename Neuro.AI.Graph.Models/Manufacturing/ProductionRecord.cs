using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ProductionRecord
{
    public Guid ProductionId { get; set; }

    public string Status { get; set; } = null!;

    public string BeginTime { get; set; } = null!;

    public string EndTime { get; set; } = null!;

    public decimal Duration { get; set; }

    public int? IsCut { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? TaskId { get; set; }

    public Guid? CreatedBy { get; set; }
}
