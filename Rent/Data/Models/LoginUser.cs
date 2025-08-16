using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class LoginUser
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public DateTime entryTime { get; set; }
}
