using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class MedicalReferral
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public string Specialization { get; set; } = null!;

    public sbyte? IsUsed { get; set; }

    public virtual User Doctor { get; set; } = null!;

    public virtual User Patient { get; set; } = null!;
}
