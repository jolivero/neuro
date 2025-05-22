using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class TurnDetail
{
    public Guid TurnDetailId { get; set; }

    public string? PeriodType { get; set; }

    public string? BeginAt { get; set; }

    public string? EndAt { get; set; }

    public decimal? Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? TurnId { get; set; }
}
