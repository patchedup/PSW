using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public int? AdminId { get; set; }

    public virtual User? Admin { get; set; }
}
