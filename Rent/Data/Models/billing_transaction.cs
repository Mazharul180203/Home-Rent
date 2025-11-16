using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class billing_transaction
{
    public long id { get; set; }

    public long subscription_id { get; set; }

    public decimal amount { get; set; }

    public string? transaction_id { get; set; }

    public string? status { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual subscription subscription { get; set; } = null!;
}
