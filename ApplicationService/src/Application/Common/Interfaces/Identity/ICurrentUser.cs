namespace Application.Common.Interfaces.Identity
{
    public interface ICurrentUser
    {
        /// <summary>
        /// Gets the ID of the current user.
        /// </summary>
        Guid? UserId { get; }
        /// <summary>
        /// Gets the role of the current user.
        /// </summary>
        string Role { get; }
    }
}
