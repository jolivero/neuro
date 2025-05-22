using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ProducedPartsRecord
{
    public Guid ProducedPartId { get; set; }

    public int? Total { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? ProductionId { get; set; }

    public Guid? PartId { get; set; }
}
