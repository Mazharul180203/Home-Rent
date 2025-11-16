using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class maintenance_request
{
    public long id { get; set; }

    public long tenant_id { get; set; }

    public long unit_id { get; set; }

    public string description { get; set; } = null!;

    public string? priority { get; set; }

    public string? status { get; set; }

    public long? approved_by { get; set; }

    public DateTime? approval_date { get; set; }

    public string? attachments { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual user? approved_byNavigation { get; set; }

    public virtual user tenant { get; set; } = null!;

    public virtual unit unit { get; set; } = null!;

    public virtual ICollection<work_order> work_orders { get; set; } = new List<work_order>();
}
