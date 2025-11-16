using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class reward
{
    public long id { get; set; }

    public long tenant_id { get; set; }

    public int points { get; set; }

    public string action_type { get; set; } = null!;

    public string? reward_type { get; set; }

    public DateTime? redeemed_at { get; set; }

    public string? details { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual user tenant { get; set; } = null!;
}
