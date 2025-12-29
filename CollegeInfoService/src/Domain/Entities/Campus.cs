using Domain.Common;

namespace Domain.Entities
{
    public class Campus : BaseEntity<Guid>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string Address { get; set; } = default!;
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public ICollection<CampusAcademicField> CampusAcademicFields { get; set; } = default!;

    }
}
