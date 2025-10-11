using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Skill
{
    public int SkillId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UsersSkill> UsersSkills { get; set; } = new List<UsersSkill>();
}
