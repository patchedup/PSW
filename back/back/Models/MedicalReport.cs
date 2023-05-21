using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class MedicalReport
{
    public int Id { get; set; }

    public int? AppointmentId { get; set; }

    public string? Diagnosis { get; set; }

    public virtual Appointment? Appointment { get; set; }
}
