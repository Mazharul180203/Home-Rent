using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class subscription
{
    public long id { get; set; }

    public long user_id { get; set; }

    public string plan_type { get; set; } = null!;

    public DateOnly start_date { get; set; }

    public DateOnly? end_date { get; set; }

    public string? status { get; set; }

    public string? details { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<billing_transaction> billing_transactions { get; set; } = new List<billing_transaction>();

    public virtual user user { get; set; } = null!;
}
