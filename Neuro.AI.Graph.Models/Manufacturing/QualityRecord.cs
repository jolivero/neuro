using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class QualityRecord
{
    public Guid QualityId { get; set; }

    public string? Type { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? NcPartId { get; set; }

    public Guid? CreatedBy { get; set; }
}
