﻿using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Part
{
    public Guid PartId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? Available { get; set; }

    public Guid? StationId { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual ICollection<ChangeRequestDetail> ChangeRequestDetails { get; set; } = new List<ChangeRequestDetail>();

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Inventory? Inventory { get; set; }

    public virtual ICollection<NonCompliantPartsRecord> NonCompliantPartsRecords { get; set; } = new List<NonCompliantPartsRecord>();

    public virtual ICollection<ProducedPartsRecord> ProducedPartsRecords { get; set; } = new List<ProducedPartsRecord>();

    public virtual ICollection<ProductionLineRecipe> ProductionLineRecipeParts { get; set; } = new List<ProductionLineRecipe>();

    public virtual ICollection<ProductionLineRecipe> ProductionLineRecipePreviousParts { get; set; } = new List<ProductionLineRecipe>();

    public virtual Station? Station { get; set; }
}
