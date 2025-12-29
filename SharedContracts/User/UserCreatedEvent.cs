namespace SharedContracts.User
{
    public class UserCreatedEvent
    {
        public Guid UserId { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}
