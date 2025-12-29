using System.Security.Cryptography.X509Certificates;

namespace Domain.Entities
{
    public class CampusAcademicField
    {
        public Guid CampusId { get; set; }
        public Campus Campus { get; set; } = default!;
        public Guid AcademicFieldId { get; set; }
        public AcademicField AcademicField { get; set; } = default!;
        public ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();
    }
}
