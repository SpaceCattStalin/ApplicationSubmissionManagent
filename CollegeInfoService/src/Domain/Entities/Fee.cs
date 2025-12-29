using Domain.Common;

namespace Domain.Entities
{
    public class Fee : BaseEntity<Guid>
    {
        public decimal Amount { get; set; }
        public string AcademicYear { get; set; } = default!;
        public int StartSemester { get; set; }
        public int EndSemester { get; set; }
        public ICollection<SpecializationFee> SpecializationFees { get; set; } = default!;
    }
}
