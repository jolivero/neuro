using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class QualityRecord
{
    public Guid QualityId { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? NcPartId { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual NonCompliantPartsRecord? NcPart { get; set; }

    public virtual ICollection<QualityClasification> QualityClasifications { get; set; } = new List<QualityClasification>();
}
