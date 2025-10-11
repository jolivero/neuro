using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class RequestCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ProductionChangeRequest> ProductionChangeRequests { get; set; } = new List<ProductionChangeRequest>();
}
