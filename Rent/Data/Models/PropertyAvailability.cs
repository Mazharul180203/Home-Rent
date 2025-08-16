using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class PropertyAvailability
{
    public int AvailabilityId { get; set; }

    public int? PropertyId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Property? Property { get; set; }
}
