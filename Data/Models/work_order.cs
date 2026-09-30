using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class work_order
{
    public long id { get; set; }

    public long request_id { get; set; }

    public long? vendor_id { get; set; }

    public decimal? cost { get; set; }

    public DateOnly? scheduled_date { get; set; }

    public DateOnly? completion_date { get; set; }

    public string? notes { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<maintenance_cost> maintenance_costs { get; set; } = new List<maintenance_cost>();

    public virtual maintenance_request request { get; set; } = null!;

    public virtual user? vendor { get; set; }
}
