using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class utility
{
    public long id { get; set; }

    public long? lease_id { get; set; }

    public long unit_id { get; set; }

    public string type { get; set; } = null!;

    public decimal amount { get; set; }

    public DateOnly period_start { get; set; }

    public DateOnly period_end { get; set; }

    public decimal? meter_reading { get; set; }

    public string? status { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual lease? lease { get; set; }

    public virtual unit unit { get; set; } = null!;
}
