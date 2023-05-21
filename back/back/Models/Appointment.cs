using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    public int InternistDataId { get; set; }

    public string Time { get; set; } = null!;

    public virtual User Doctor { get; set; } = null!;

    public virtual InternistDatum IdNavigation { get; set; } = null!;

    public virtual ICollection<MedicalReport> MedicalReports { get; } = new List<MedicalReport>();

    public virtual User Patient { get; set; } = null!;
}
