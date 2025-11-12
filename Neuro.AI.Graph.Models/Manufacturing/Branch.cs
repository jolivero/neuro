using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Branch
{
    public int BranchId { get; set; }

    public string? BranchName { get; set; }

    public string? BranchAddress { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? CompanyId { get; set; }

    public int? Available { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<Machine> Machines { get; set; } = new List<Machine>();

    public virtual ICollection<Part> Parts { get; set; } = new List<Part>();

    public virtual ICollection<ProductionLine> ProductionLines { get; set; } = new List<ProductionLine>();

    public virtual ICollection<Station> Stations { get; set; } = new List<Station>();

    public virtual ICollection<Turn> Turns { get; set; } = new List<Turn>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
