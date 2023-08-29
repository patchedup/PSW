namespace back.Dtos;


public partial class ReportDTO
{
    public long Id { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public virtual long AppointmentId { get; set; }

}
