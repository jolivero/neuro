using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class TurnDetail
{
    public int TurnDetailId { get; set; }

    public string? PeriodType { get; set; }

    public TimeOnly? BeginAt { get; set; }

    public TimeOnly? EndAt { get; set; }

    public TimeOnly? Duration { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Available { get; set; }

    public int? TurnId { get; set; }

    public virtual Turn? Turn { get; set; }
}
