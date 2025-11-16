using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class tenant_onboarding
{
    public long id { get; set; }

    public long user_id { get; set; }

    public string step { get; set; } = null!;

    public string? status { get; set; }

    public string? details { get; set; }

    public DateTime? completed_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual user user { get; set; } = null!;
}
