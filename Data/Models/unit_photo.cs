using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class unit_photo
{
    public long id { get; set; }

    public long unit_id { get; set; }

    public string photo_path { get; set; } = null!;

    public string? original_name { get; set; }

    public string? category { get; set; }

    public bool? is_main { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual unit unit { get; set; } = null!;
}
