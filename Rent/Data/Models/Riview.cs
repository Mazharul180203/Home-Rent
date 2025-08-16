using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Riview
{
    public int ReviewId { get; set; }

    public int? PropertyId { get; set; }

    public int? RenterId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Property? Property { get; set; }

    public virtual User? Renter { get; set; }
}
