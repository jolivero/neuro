using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Log
{
    public int LogId { get; set; }

    public Guid? UserIdRef { get; set; }

    public string UserName { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string Area { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? Desc0 { get; set; }

    public string? Desc1 { get; set; }

    public string? Desc2 { get; set; }

    public string? Desc3 { get; set; }

    public int? BranchId { get; set; }

    public virtual Branch? Branch { get; set; }
}
