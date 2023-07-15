using System;
using System.Collections.Generic;

namespace back.Models;

public partial class User
{
    public long Id { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public ulong IsBlocked { get; set; }

    public string? LastName { get; set; }

    public int NumberOfPenalties { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    private ulong? is_female;

    public string? Specialization { get; set; }

    public long? AssignedGeneralPracticeDoctorId { get; set; }

    public virtual ICollection<Appointment> AppointmentDoctors { get; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentPatients { get; } = new List<Appointment>();

    public virtual User? AssignedGeneralPracticeDoctor { get; set; }

    public virtual ICollection<Blog> Blogs { get; } = new List<Blog>();

    public virtual ICollection<InternistData> InternistData { get; } = new List<InternistData>();

    public virtual ICollection<User> InverseAssignedGeneralPracticeDoctor { get; } = new List<User>();

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();

    public virtual ICollection<Referral> Referrals { get; } = new List<Referral>();
    public ulong? Is_female { get => is_female; set => is_female = value; }
}
