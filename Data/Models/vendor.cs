using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class vendor
{
    public long id { get; set; }

    public long user_id { get; set; }

    public string expertise { get; set; } = null!;

    public decimal? rating { get; set; }

    public string? availability { get; set; }

    public string? details { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual user user { get; set; } = null!;
}
