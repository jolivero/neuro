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

    public DateTime? UpdatedAt { get; set; }

    public Guid? TaskId { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<NonCompliantPartsRecord> NonCompliantPartsRecords { get; set; } = new List<NonCompliantPartsRecord>();

    public virtual ICollection<ProducedPartsRecord> ProducedPartsRecords { get; set; } = new List<ProducedPartsRecord>();

    public virtual DailyTask? Task { get; set; }
}
