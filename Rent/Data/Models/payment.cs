using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class payment
{
    public long id { get; set; }

    public long lease_id { get; set; }

    public decimal amount { get; set; }

    public decimal? partial_amount { get; set; }

    public decimal? late_fee { get; set; }

    public DateOnly due_date { get; set; }

    public DateOnly? paid_date { get; set; }

    public string method { get; set; } = null!;

    public string? transaction_id { get; set; }

    public string? status { get; set; }

    public string? receipt_pdf { get; set; }

    public DateTime? deleted_at { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual lease lease { get; set; } = null!;
}
