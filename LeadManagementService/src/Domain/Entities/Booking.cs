using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Booking : BaseEntity<Guid>
    {
        public string UserFullName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string UserPhoneNumber { get; set; } = default!;
        public string? InterestedCampus { get; set; }
        public string? InterestedAcademicField { get; set; }
        public string? InterestedSpecialization { get; set; }
        //public string? InterestedCourse { get; set; }
        public string Location { get; set; } = default!; // Location of the booking (e.g., campus, online)
        public string Reason { get; set; } = default!;
        public BookingStatus Status { get; set; }

        // Microservice-safe foreign references
        public Guid? CreatedByUserId { get; set; }           // External user who made the request
        public Guid? ClaimedByConsultantId { get; set; }    // Consultant who claimed it (optional)
        public DateTime ClaimedAt { get; set; }
        public DateTime? FirstContactAttempt { get; set; } // When the first contact was attempted
        public DateTime? LastContactAttempt { get; set; } // When the last contact was attempted
        public ContactStatus ContactStatus { get; set; } = ContactStatus.NoContact; // Status of contact with the user
        public int ContactAttemptsCount { get; set; } = 0; // Number of contact attempts made
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // When the booking was created

        public ICollection<BookingHistory> BookingHistories { get; set; } = new List<BookingHistory>();
    }
}
