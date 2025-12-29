namespace SharedContracts.User
{
    public class UserAlreadyExistApplicationEvent
    {
        public string ApplicationId { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
    }
}
