using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class maintenance_cost
{
    public long id { get; set; }

    public long work_order_id { get; set; }

    public string cost_type { get; set; } = null!;

    public decimal amount { get; set; }

    public string? description { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual work_order work_order { get; set; } = null!;
}
