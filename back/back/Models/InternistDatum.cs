using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class InternistDatum
{
    public int Id { get; set; }

    public string? BloodPressure { get; set; }

    public int? BloodSuggar { get; set; }

    public int? BodyFat { get; set; }

    public int? Weight { get; set; }

    public string? MeasuredDate { get; set; }

    public int? PatientId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual User? Patient { get; set; }
}
