using System;
using System.Collections.Generic;

namespace back.Models;

public partial class InternistDatum
{
    public long Id { get; set; }

    public string? BloodPressure { get; set; }

    public int BloodSugar { get; set; }

    public int BodyFat { get; set; }

    public string? Date { get; set; }

    public int Weight { get; set; }

    public long? UserId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();

    public virtual User? User { get; set; }
}
