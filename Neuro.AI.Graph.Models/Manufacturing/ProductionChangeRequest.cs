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

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? ChangesRequestId { get; set; }

    public Guid? RequestingUserId { get; set; }

    public Guid? ApprovalUserId { get; set; }

    public Guid? NcPartId { get; set; }

    public virtual User? ApprovalUser { get; set; }

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetails { get; set; } = new List<ChangeRequestDetail>();

    public virtual ChangesRequest? ChangesRequest { get; set; }

    public virtual NonCompliantPartsRecord? NcPart { get; set; }

    public virtual User? RequestingUser { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
