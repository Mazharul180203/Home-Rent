using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class notification
{
    public long id { get; set; }

    public long user_id { get; set; }

    public string type { get; set; } = null!;

    public string channel { get; set; } = null!;

    public bool? enabled { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual user user { get; set; } = null!;
}
