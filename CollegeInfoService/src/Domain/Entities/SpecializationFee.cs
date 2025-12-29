namespace Domain.Entities
{
    public class SpecializationFee
    {
        public Guid SpecializationId { get; set; }
        public Specialization Specialization { get; set; } = default!;
        public Guid FeeId { get; set; }
        public Fee Fee { get; set; } = default!;
    }
}
