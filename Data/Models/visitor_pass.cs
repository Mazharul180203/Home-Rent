using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class visitor_pass
{
    public long id { get; set; }

    public long tenant_id { get; set; }

    public string visitor_name { get; set; } = null!;

    public DateOnly visit_date { get; set; }

    public string? qr_code { get; set; }

    public string? status { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual user tenant { get; set; } = null!;
}
