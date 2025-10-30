using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ProductionChangeRequest
{
    public int RequestId { get; set; }

    public string Reason { get; set; } = null!;

    public string? OriginRequest { get; set; }

    public DateTime? RequestedAt { get; set; }

    public DateTime? ResponseAt { get; set; }

    public string? Response { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CategoryId { get; set; }

    public Guid? RequestingUserId { get; set; }

    public Guid? ApprovalUserId { get; set; }

    public int? NcPartId { get; set; }

    public int? MonthId { get; set; }

    public int? DayId { get; set; }

    public int? TaskId { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public virtual User? ApprovalUser { get; set; }

    public virtual RequestCategory? Category { get; set; }

    public virtual ICollection<ChangePlanningDay> ChangePlanningDays { get; set; } = new List<ChangePlanningDay>();

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetails { get; set; } = new List<ChangeRequestDetail>();

    public virtual DailyPlanning? Day { get; set; }

    public virtual MonthlyPlanning? Month { get; set; }

    public virtual NonCompliantPartsRecord? NcPart { get; set; }

    public virtual User? RequestingUser { get; set; }

    public virtual DailyTask? Task { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
