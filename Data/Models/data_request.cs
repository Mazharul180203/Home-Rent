using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class data_request
{
    public long id { get; set; }

    public long user_id { get; set; }

    public string request_type { get; set; } = null!;

    public string? status { get; set; }

    public DateTime? submitted_at { get; set; }

    public DateTime? resolved_at { get; set; }

    public string? details { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual user user { get; set; } = null!;
}
