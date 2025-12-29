using Application.Common.Interfaces.Service;
using DnsClient;

namespace Infrastructure.Services
{
    public class EmailValidator : IEmailValidator
    {
        public async Task<bool> HasValidMxRecordAsync(string email)
        {
            var domain = email.Split('@').LastOrDefault();
            var lookup = new LookupClient();

            var result = await lookup.QueryAsync(domain, QueryType.MX);
            return result.Answers.MxRecords().Any();
        }
    }
}
