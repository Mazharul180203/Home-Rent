using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class communication
{
    public long id { get; set; }

    public long building_id { get; set; }

    public long sender_id { get; set; }

    public long recipient_id { get; set; }

    public string type { get; set; } = null!;

    public string content { get; set; } = null!;

    public DateTime? sent_at { get; set; }

    public DateTime? read_at { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual property building { get; set; } = null!;

    public virtual user recipient { get; set; } = null!;

    public virtual user sender { get; set; } = null!;
}
