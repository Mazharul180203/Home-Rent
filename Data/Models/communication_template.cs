using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class communication_template
{
    public long id { get; set; }

    public string name { get; set; } = null!;

    public string type { get; set; } = null!;

    public string content { get; set; } = null!;

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }
}
