using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ProductionLineRecipe
{
    public int RecipeId { get; set; }

    public int? LineId { get; set; }

    public int? GroupId { get; set; }

    public int? StationId { get; set; }

    public int? MachineId { get; set; }

    public int? PartId { get; set; }

    public int? PreviousPartId { get; set; }

    public decimal? RequiredQuantity { get; set; }

    public int? StepOrder { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Group? Group { get; set; }

    public virtual ProductionLine? Line { get; set; }

    public virtual Machine? Machine { get; set; }

    public virtual Part? Part { get; set; }

    public virtual Part? PreviousPart { get; set; }

    public virtual ICollection<ProductionLineRecipeMaterial> ProductionLineRecipeMaterials { get; set; } = new List<ProductionLineRecipeMaterial>();

    public virtual Station? Station { get; set; }
}
