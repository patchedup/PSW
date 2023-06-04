using System;
using System.Collections.Generic;

namespace back.Models;

public partial class Referral
{
    public long Id { get; set; }

    public ulong IsUsed { get; set; }

    public long? ForDoctorId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();

    public virtual User? ForDoctor { get; set; }
}
