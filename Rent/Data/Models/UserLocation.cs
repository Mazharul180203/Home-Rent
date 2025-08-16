using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class UserLocation
{
    public int LocationId { get; set; }

    public int? UserId { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }
}
