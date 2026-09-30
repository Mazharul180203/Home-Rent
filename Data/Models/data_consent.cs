using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class data_consent
{
    public long id { get; set; }

    public long user_id { get; set; }

    public string consent_type { get; set; } = null!;

    public DateTime? granted_at { get; set; }

    public DateTime? revoked_at { get; set; }

    public string? details { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual user user { get; set; } = null!;
}
