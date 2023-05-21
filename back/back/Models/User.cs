using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public sbyte? IsBlocked { get; set; }

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public int? NumberOfPenalties { get; set; }

    public string? Role { get; set; }

    public string? Specialization { get; set; }

    public int? AssignedGeneralPraticeId { get; set; }

    public virtual ICollection<Appointment> AppointmentDoctors { get; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentPatients { get; } = new List<Appointment>();

    public virtual User? AssignedGeneralPratice { get; set; }

    public virtual ICollection<Blog> Blogs { get; } = new List<Blog>();

    public virtual ICollection<InternistDatum> InternistData { get; } = new List<InternistDatum>();

    public virtual ICollection<User> InverseAssignedGeneralPratice { get; } = new List<User>();

    public virtual ICollection<MedicalReferral> MedicalReferralDoctors { get; } = new List<MedicalReferral>();

    public virtual ICollection<MedicalReferral> MedicalReferralPatients { get; } = new List<MedicalReferral>();

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
}
