using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class OperatorChangeRequest
{
    public Guid RequestId { get; set; }

    public Guid UserId { get; set; }
}
