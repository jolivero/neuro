using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ProductioLineRecipeMaterial
{
    public int MaterialId { get; set; }

    public int? PreviousPartId { get; set; }

    public decimal? RequiredQuantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? MaterialOrder { get; set; }

    public int? RecipeId { get; set; }

    public virtual Part? PreviousPart { get; set; }

    public virtual ProductionLineRecipe? Recipe { get; set; }
}
