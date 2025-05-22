using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ChangeRequestDetail
{
    public Guid DetailId { get; set; }

    public string RequestType { get; set; } = null!;

    public string? Description { get; set; }

    public int? HoursQuantity { get; set; }

    public Guid? RequestId { get; set; }

    public Guid? StationId { get; set; }

    public Guid? MachineId { get; set; }

    public Guid? PartId { get; set; }
}
