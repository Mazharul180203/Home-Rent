using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class maintenance_schedule
{
    public long id { get; set; }

    public long unit_id { get; set; }

    public string type { get; set; } = null!;

    public string frequency { get; set; } = null!;

    public DateOnly next_due_date { get; set; }

    public DateOnly? last_completed_date { get; set; }

    public long? vendor_id { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual unit unit { get; set; } = null!;

    public virtual user? vendor { get; set; }
}
