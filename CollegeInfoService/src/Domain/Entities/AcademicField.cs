using Domain.Common;

namespace Domain.Entities
{
    public class AcademicField : BaseEntity<Guid>
    {
        public string Name { get; set; } = default!;
        public ICollection<CampusAcademicField> CampusAcademicFields { get; set; } = default!;
    }
}
