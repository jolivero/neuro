using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class UsersSkill
{
    public Guid SkillId { get; set; }

    public Guid UserId { get; set; }

    public string? SkillLevel { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
