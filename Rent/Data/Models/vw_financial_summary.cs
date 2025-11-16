using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class vw_financial_summary
{
    public long property_id { get; set; }

    public string property_name { get; set; } = null!;

    public int? total_units { get; set; }

    public int? occupied_units { get; set; }

    public decimal? total_revenue { get; set; }
}
