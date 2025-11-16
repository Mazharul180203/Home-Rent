using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class unit
{
    public long id { get; set; }

    public long building_id { get; set; }

    public string unit_number { get; set; } = null!;

    public string? status { get; set; }

    public decimal? rent_amount { get; set; }

    public string? description { get; set; }

    public int? square_feet { get; set; }

    public int? bedrooms { get; set; }

    public int? bathrooms { get; set; }

    public string? photos { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual property building { get; set; } = null!;

    public virtual ICollection<lease> leases { get; set; } = new List<lease>();

    public virtual ICollection<maintenance_request> maintenance_requests { get; set; } = new List<maintenance_request>();

    public virtual ICollection<maintenance_schedule> maintenance_schedules { get; set; } = new List<maintenance_schedule>();

    public virtual ICollection<user> users { get; set; } = new List<user>();

    public virtual ICollection<utility> utilities { get; set; } = new List<utility>();
}
