using Domain.Common;

namespace Domain.Entities
{
    public class Specialization : BaseEntity<Guid>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public CampusAcademicField CampusAcademicField { get; set; } = default!;
        public ICollection<SpecializationFee> SpecializationFees { get; set; } = default!;
    }
}
