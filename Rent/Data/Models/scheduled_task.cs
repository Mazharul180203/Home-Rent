using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class scheduled_task
{
    public long id { get; set; }

    public string task_type { get; set; } = null!;

    public string entity_type { get; set; } = null!;

    public long entity_id { get; set; }

    public string frequency { get; set; } = null!;

    public DateTime next_execution { get; set; }

    public DateTime? last_execution { get; set; }

    public string? status { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }
}
