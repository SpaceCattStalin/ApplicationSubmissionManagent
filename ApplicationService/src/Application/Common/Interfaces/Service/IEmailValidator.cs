namespace Application.Common.Interfaces.Service
{
    public interface IEmailValidator
    {
        /// <summary>
        /// Checks if the provided email address is valid.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Boolean</returns>
        Task<bool> HasValidMxRecordAsync(string email);
    }
}
