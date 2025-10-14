using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ProductionLine
{
    public int LineId { get; set; }

    public string Name { get; set; } = null!;

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Available { get; set; }

    public int? CompanyId { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetailCurrentLines { get; set; } = new List<ChangeRequestDetail>();

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetailNewLines { get; set; } = new List<ChangeRequestDetail>();

    public virtual Company? Company { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<MonthlyPlanning> MonthlyPlannings { get; set; } = new List<MonthlyPlanning>();

    public virtual ICollection<ProductionLineRecipe> ProductionLineRecipes { get; set; } = new List<ProductionLineRecipe>();
}
