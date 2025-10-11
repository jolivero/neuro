using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class QualityRecord
{
    public int QualityId { get; set; }

    public string? Type { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? NcPartId { get; set; }

    public int? CreatedBy { get; set; }

    public virtual NonCompliantPartsRecord? NcPart { get; set; }

    public virtual ICollection<QualityClasification> QualityClasifications { get; set; } = new List<QualityClasification>();
}
