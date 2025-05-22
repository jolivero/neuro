using System;
using System.Collections.Generic;

namespace Neuro.AI.Graph.Models.Manufacturing;

public partial class Company
{
    public Guid CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string CompanyRuc { get; set; } = null!;

    public string? CompanyAddress { get; set; }

    public string? CompanyPhone { get; set; }

    public string? CompanyWeb { get; set; }

    public string? CompanyLogo { get; set; }

    public string? CompanyColors { get; set; }

    public string? ContactName { get; set; }

    public string? ContactPhone { get; set; }

    public string? ContactEmail { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
