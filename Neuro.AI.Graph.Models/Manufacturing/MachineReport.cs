using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class MachineReport
{
    public int ReportId { get; set; }

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? StationId { get; set; }

    public int? MachineId { get; set; }

    public int? OperatorId { get; set; }

    public int? TechnicalId { get; set; }

    public virtual Machine? Machine { get; set; }

    public virtual User? Operator { get; set; }

    public virtual Station? Station { get; set; }

    public virtual User? Technical { get; set; }
}
