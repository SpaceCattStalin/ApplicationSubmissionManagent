namespace SharedContracts.Booking
{
    public class UserWithClaimedBookingEvent
    {
        public Guid ConsultantId { get; set; }
        public Guid UserId { get; set; }
    }
}
