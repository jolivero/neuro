using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Turn
{
    public int TurnId { get; set; }

    public string? Name { get; set; }

    public TimeOnly? Duration { get; set; }

    public TimeOnly? ProductiveTime { get; set; }

    public TimeOnly? PauseTime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Available { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetailCurrentTurns { get; set; } = new List<ChangeRequestDetail>();

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetailNewTurns { get; set; } = new List<ChangeRequestDetail>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<DailyTask> DailyTasks { get; set; } = new List<DailyTask>();

    public virtual ICollection<TurnDetail> TurnDetails { get; set; } = new List<TurnDetail>();
}
