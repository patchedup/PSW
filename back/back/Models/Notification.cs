using System;
using System.Collections.Generic;

namespace back.Models;

public partial class Notification
{
    public long Id { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public long? AdminId { get; set; }

    public virtual User? Admin { get; set; }
}
