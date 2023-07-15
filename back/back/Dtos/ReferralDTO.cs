namespace back.Dtos
{
    public class ReferralDTO
    {
        public long Id { get; set; }

        public ulong IsUsed { get; set; }

        public long? ForDoctorId { get; set; }

        public long? AppointmentId { get; set; }
    }
}
