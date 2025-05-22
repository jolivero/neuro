using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class ChangesRequest
{
    public Guid ChangesRequestId { get; set; }

    public string Name { get; set; } = null!;

    public string? Descripcion { get; set; }
}
