using Domain.Common;

namespace Domain.Entities
{
    public class BookingHistory : BaseEntity<Guid>
    {
        public string Content { get; set; } = default!;
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = default!;
        public Guid? PerformedByUserId { get; set; } // User who performed the action
        public DateTime CreatedAt { get; set; }
        public string? PreviousValue { get; set; }
        public string? NewValue { get; set; }

    }
}
