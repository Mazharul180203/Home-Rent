using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class access_log
{
    public long id { get; set; }

    public long user_id { get; set; }

    public string action { get; set; } = null!;

    public string entity_type { get; set; } = null!;

    public long? entity_id { get; set; }

    public DateTime? timestamp { get; set; }

    public string? ip_address { get; set; }

    public string? details { get; set; }

    public DateTime? created_at { get; set; }

    public virtual user user { get; set; } = null!;
}
