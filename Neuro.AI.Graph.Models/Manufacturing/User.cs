using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class User
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DocumentId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? BloodType { get; set; }

    public string EmployeeNumber { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public Guid? CompanyId { get; set; }

    public Guid? RolId { get; set; }

    public virtual Company? Company { get; set; }

    public virtual Role? Rol { get; set; }

    public virtual ICollection<UsersSkill> UsersSkills { get; set; } = new List<UsersSkill>();
}
