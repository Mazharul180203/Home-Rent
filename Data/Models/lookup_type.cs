using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class lookup_type
{
    public long id { get; set; }

    public string category { get; set; } = null!;

    public string value { get; set; } = null!;

    public bool? is_active { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }
}
