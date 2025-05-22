using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Station
{
    public Guid StationId { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? GroupId { get; set; }

    public Guid? CreatedBy { get; set; }
}
