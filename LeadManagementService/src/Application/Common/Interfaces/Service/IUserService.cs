namespace Application.Common.Interfaces.Service
{
    public interface IUserService
    {
        /// <summary>
        /// Checks if a user exists by their email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Boolean</returns>
        Task<bool> UserExistsByEmailAsync(string email);
    }
}
