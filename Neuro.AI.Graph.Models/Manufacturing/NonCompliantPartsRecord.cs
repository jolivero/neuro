using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class NonCompliantPartsRecord
{
    public Guid NcPartId { get; set; }

    public int? Total { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? PartId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? TaskId { get; set; }

    public virtual Part? Part { get; set; }

    public virtual ICollection<ProductionChangeRequest> ProductionChangeRequests { get; set; } = new List<ProductionChangeRequest>();

    public virtual ICollection<QualityRecord> QualityRecords { get; set; } = new List<QualityRecord>();

    public virtual DailyTask? Task { get; set; }
}
