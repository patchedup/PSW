using System;
using System.Collections.Generic;

namespace back.Models;

public partial class Appointment
{
    public long Id { get; set; }

    public string? Time { get; set; }

    public long? DoctorId { get; set; }

    public long? MeasuredInternistDataId { get; set; }

    public long? PatientId { get; set; }

    public long? ReferralId { get; set; }

    public long? ReportId { get; set; }

    public virtual User? Doctor { get; set; }

    public virtual InternistDatum? MeasuredInternistData { get; set; }

    public virtual User? Patient { get; set; }

    public virtual Referral? Referral { get; set; }

    public virtual Report? Report { get; set; }
}
