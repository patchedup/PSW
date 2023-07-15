using System;
using System.Collections.Generic;

namespace back.Models;

public partial class Report
{
    public long Id { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();

}
