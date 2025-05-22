using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Part
{
    public Guid PartId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public Guid? StationId { get; set; }

    public Guid? CreatedBy { get; set; }
}
