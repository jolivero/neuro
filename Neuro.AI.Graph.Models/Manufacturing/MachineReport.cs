using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class MachineReport
{
    public Guid ReportId { get; set; }

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public Guid? StationId { get; set; }

    public Guid? MachineId { get; set; }

    public Guid? OperatorId { get; set; }

    public Guid? TechnicalId { get; set; }
}
