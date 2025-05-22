using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class QualityClasification
{
    public Guid ClasificationId { get; set; }

    public int MinParts { get; set; }

    public int Reprocess { get; set; }

    public int Scrap { get; set; }

    public string? Reason { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? QualityId { get; set; }
}
