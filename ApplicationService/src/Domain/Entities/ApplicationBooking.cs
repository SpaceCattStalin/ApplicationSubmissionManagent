using Domain.Common;
using Domain.Enum;

namespace Domain.Entities
{
    public class ApplicationBooking : BaseEntity<Guid>
    {
        public Guid CreatedByUserId { get; set; }
        public Guid? ClaimedByConsultantId { get; set; }
        public DateTime? ClaimedAt { get; set; }
        public string UserFullName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string UserPhoneNumber { get; set; } = default!;
        public DateTime DateOfBirth { get; set; }
        public string Province { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string School { get; set; } = default!;
        public int GraduationYear { get; set; }
        public string Campus { get; set; } = default!;
        public string InterestedAcademicField { get; set; } = default!;
        public int MathScore { get; set; }
        public int EnglishScore { get; set; }
        public int LiteratureScore { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Waiting;
        public ICollection<ApplicationBookingHistory> Histories { get; set; } = new List<ApplicationBookingHistory>();
    }
}
