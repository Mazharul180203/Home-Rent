using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class tenant_preference
{
    public long id { get; set; }

    public long user_id { get; set; }

    public string preferences { get; set; } = null!;

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual user user { get; set; } = null!;
}
