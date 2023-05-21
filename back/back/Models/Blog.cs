using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class Blog
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public int DoctorId { get; set; }

    public virtual User Doctor { get; set; } = null!;
}
