using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class PropertyImage
{
    public int ImageId { get; set; }

    public int? PropertyId { get; set; }

    public string? ImageUrl { get; set; }

    public string? Caption { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Property? Property { get; set; }
}
