using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class QualityClasification
{
    public int ClasificationId { get; set; }

    public int MinParts { get; set; }

    public int Reprocess { get; set; }

    public int Scrap { get; set; }

    public string? Reason { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? QualityId { get; set; }

    public virtual QualityRecord? Quality { get; set; }
}
