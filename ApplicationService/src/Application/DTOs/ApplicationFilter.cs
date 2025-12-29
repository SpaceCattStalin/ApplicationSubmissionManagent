using Domain.Enum;

namespace Application.DTOs
{
    public class ApplicationFilter
    {
        public string? Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? School { get; set; }
        public string? GraduationYear { get; set; }
        public string? Province { get; set; }
        public string? Campus { get; set; }
        public string? AcademicField { get; set; }
        public Guid? ClaimedByConsultantId { get; set; }
        public ApplicationStatus? Status { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }

    }
}
