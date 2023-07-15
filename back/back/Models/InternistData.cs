using System;
using System.Collections.Generic;

namespace back.Models;

public partial class InternistData
{
    private string? menstruationEndDate;

    public long Id { get; set; }

    public string? BloodPressure { get; set; }

    public int BloodSugar { get; set; }

    public int BodyFat { get; set; }

    public string? Date { get; set; }

    public int Weight { get; set; }

    public long? UserId { get; set; }


    private string? menstruation_end_date;
    private string? menstruation_start_date;

    public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();

    public virtual User? User { get; set; }
    public string? Menstruation_end_date { get => menstruation_end_date; set => menstruation_end_date = value; }
    public string? Menstruation_start_date { get => menstruation_start_date; set => menstruation_start_date = value; }
}
