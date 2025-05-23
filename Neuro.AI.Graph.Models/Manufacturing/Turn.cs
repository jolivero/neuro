using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Turn
{
    public Guid TurnId { get; set; }

    public string? Name { get; set; }

    public decimal? Duration { get; set; }

    public decimal? ProductiveTime { get; set; }

    public decimal? PauseTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<TurnDetail> TurnDetails { get; set; } = new List<TurnDetail>();
}
