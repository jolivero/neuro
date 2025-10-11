using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class NonCompliantPartsRecord
{
    public int NcPartId { get; set; }

    public int? Total { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? TaskId { get; set; }

    public int? PartId { get; set; }

    public virtual Part? Part { get; set; }

    public virtual ICollection<ProductionChangeRequest> ProductionChangeRequests { get; set; } = new List<ProductionChangeRequest>();

    public virtual ICollection<QualityRecord> QualityRecords { get; set; } = new List<QualityRecord>();

    public virtual DailyTask? Task { get; set; }
}
