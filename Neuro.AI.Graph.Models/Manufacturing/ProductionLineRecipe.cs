using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ProductionLineRecipe
{
    public Guid RecipeId { get; set; }

    public Guid? LineId { get; set; }

    public Guid? GroupId { get; set; }

    public Guid? StationId { get; set; }

    public Guid? MachineId { get; set; }

    public Guid? PartId { get; set; }

    public Guid? PreviousPartId { get; set; }

    public decimal RequiredQuantity { get; set; }

    public virtual Group? Group { get; set; }

    public virtual ProductionLine? Line { get; set; }

    public virtual Machine? Machine { get; set; }

    public virtual Part? Part { get; set; }

    public virtual Part? PreviousPart { get; set; }

    public virtual Station? Station { get; set; }
}
