using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class tenant_feedback
{
    public long id { get; set; }

    public long user_id { get; set; }

    public long building_id { get; set; }

    public int rating { get; set; }

    public string? comments { get; set; }

    public DateTime? submitted_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual property building { get; set; } = null!;

    public virtual user user { get; set; } = null!;
}
