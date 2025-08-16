using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class OwnerContact
{
    public int ContactId { get; set; }

    public int? OwnerId { get; set; }

    public string? ContactType { get; set; }

    public string? ContactValue { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? Owner { get; set; }
}
