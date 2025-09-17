using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class User
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? BloodType { get; set; }

    public string EmployeeNumber { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Available { get; set; }

    public Guid? CompanyId { get; set; }

    public string? Rol { get; set; }

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetailCurrentUsers { get; set; } = new List<ChangeRequestDetail>();

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetailNewUsers { get; set; } = new List<ChangeRequestDetail>();

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual Company? Company { get; set; }

    public virtual ICollection<DailyTask> DailyTasks { get; set; } = new List<DailyTask>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<MachineReport> MachineReportOperators { get; set; } = new List<MachineReport>();

    public virtual ICollection<MachineReport> MachineReportTechnicals { get; set; } = new List<MachineReport>();

    public virtual ICollection<Machine> Machines { get; set; } = new List<Machine>();

    public virtual ICollection<MonthlyPlanning> MonthlyPlannings { get; set; } = new List<MonthlyPlanning>();

    public virtual ICollection<Part> Parts { get; set; } = new List<Part>();

    public virtual ICollection<ProductionChangeRequest> ProductionChangeRequestApprovalUsers { get; set; } = new List<ProductionChangeRequest>();

    public virtual ICollection<ProductionChangeRequest> ProductionChangeRequestRequestingUsers { get; set; } = new List<ProductionChangeRequest>();

    public virtual ICollection<ProductionLine> ProductionLines { get; set; } = new List<ProductionLine>();

    public virtual ICollection<ProductionRecord> ProductionRecords { get; set; } = new List<ProductionRecord>();

    public virtual ICollection<Station> Stations { get; set; } = new List<Station>();

    public virtual ICollection<Turn> Turns { get; set; } = new List<Turn>();

    public virtual ICollection<UsersSkill> UsersSkills { get; set; } = new List<UsersSkill>();

    public virtual ICollection<ProductionChangeRequest> Requests { get; set; } = new List<ProductionChangeRequest>();
}
