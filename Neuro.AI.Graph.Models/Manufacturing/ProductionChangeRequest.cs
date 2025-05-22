using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ProductionChangeRequest
{
    public Guid RequestId { get; set; }

    public string Reason { get; set; } = null!;

    public string? OriginRequest { get; set; }

    public DateTime? RequestedAt { get; set; }

    public DateTime? ResponseAt { get; set; }

    public string? Response { get; set; }

    public string? Status { get; set; }

    public Guid? ChangesRequestId { get; set; }

    public Guid? RequestingUserId { get; set; }

    public Guid? ApprovalUserId { get; set; }

    public Guid? NcPartId { get; set; }
}
