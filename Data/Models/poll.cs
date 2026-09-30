using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class poll
{
    public long id { get; set; }

    public long building_id { get; set; }

    public string question { get; set; } = null!;

    public string options { get; set; } = null!;

    public DateOnly start_date { get; set; }

    public DateOnly? end_date { get; set; }

    public string? votes { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual property building { get; set; } = null!;
}
