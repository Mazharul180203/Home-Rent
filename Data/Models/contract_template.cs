using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class contract_template
{
    public long id { get; set; }

    public string name { get; set; } = null!;

    public string content { get; set; } = null!;

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<lease> leases { get; set; } = new List<lease>();
}
