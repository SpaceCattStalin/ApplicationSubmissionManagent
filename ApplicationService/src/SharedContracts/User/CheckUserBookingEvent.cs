namespace SharedContracts.User
{
    public class CheckUserBookingEvent
    {
        public Guid UserId { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}
