namespace SharedContracts.Application
{
    public class UserCreatedFromApplicationEvent
    {
        public Guid UserId { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}
