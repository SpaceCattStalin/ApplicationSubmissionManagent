using Domain.Common;

namespace Domain.Entities
{
    public class ApplicationBookingHistory : BaseEntity<Guid>
    {
        public string Content { get; set; } = default!;
        public Guid ApplicationBookingId { get; set; }
        public Guid? PerformedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PreviousValue { get; set; }
        public string? NewValue { get; set; }
        public ApplicationBooking ApplicationBooking { get; set; } = default!;
    }
}
