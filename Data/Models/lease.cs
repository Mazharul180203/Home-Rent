using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class lease
{
    public long id { get; set; }

    public long tenant_id { get; set; }

    public long unit_id { get; set; }

    public long? contract_template_id { get; set; }

    public DateOnly start_date { get; set; }

    public DateOnly? end_date { get; set; }

    public decimal rent_amount { get; set; }

    public decimal? deposit { get; set; }

    public string terms { get; set; } = null!;

    public string? e_signature { get; set; }

    public string? status { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual contract_template? contract_template { get; set; }

    public virtual ICollection<payment> payments { get; set; } = new List<payment>();

    public virtual user tenant { get; set; } = null!;

    public virtual unit unit { get; set; } = null!;

    public virtual ICollection<utility> utilities { get; set; } = new List<utility>();
}
