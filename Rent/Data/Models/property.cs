using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class property
{
    public long id { get; set; }

    public long owner_id { get; set; }

    public string name { get; set; } = null!;

    public string address { get; set; } = null!;

    public string type { get; set; } = null!;

    public int? units_count { get; set; }

    public string? amenities { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<communication> communications { get; set; } = new List<communication>();

    public virtual user owner { get; set; } = null!;

    public virtual ICollection<poll> polls { get; set; } = new List<poll>();

    public virtual ICollection<tenant_feedback> tenant_feedbacks { get; set; } = new List<tenant_feedback>();

    public virtual ICollection<unit> units { get; set; } = new List<unit>();

    public virtual ICollection<user> users { get; set; } = new List<user>();
}
