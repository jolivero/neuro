using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Group
{
    public int GroupId { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Available { get; set; }

    public int? LineId { get; set; }

    public Guid? CreatedBy { get; set; }

    public int? BranchId { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetailCurrentGroups { get; set; } = new List<ChangeRequestDetail>();

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetailNewGroups { get; set; } = new List<ChangeRequestDetail>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ProductionLine? Line { get; set; }

    public virtual ICollection<ProductionLineRecipe> ProductionLineRecipes { get; set; } = new List<ProductionLineRecipe>();

    public virtual ICollection<Station> Stations { get; set; } = new List<Station>();
}
