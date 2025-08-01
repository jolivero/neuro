﻿using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Inventory
{
    public Guid InventoryId { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? PartId { get; set; }

    public virtual Part? Part { get; set; }
}
