namespace back.Models
{
    public partial class Donation
    {
        public long Id { get; set; }

        public string? HospitalName { get; set; }

        public string? Time { get; set; }

        public ulong IsArchived { get; set; }

        public ulong ShouldPublish { get; set; }

        public long? PatientId { get; set; }

        private ulong? is_archived;
        public ulong? Is_Archived { get => is_archived; set => is_archived = value; }

        private ulong? should_publish;
        public ulong? Should_publish { get => should_publish; set => should_publish = value; }

        public virtual User? Patient { get; set; }

    }
}
