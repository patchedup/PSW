using System;
using System.Collections.Generic;

namespace back.Models;

public partial class Blog
{
    public long Id { get; set; }

    public string? Content { get; set; }

    public string? Title { get; set; }

    public long? UserId { get; set; }

    public virtual User? User { get; set; }
}
