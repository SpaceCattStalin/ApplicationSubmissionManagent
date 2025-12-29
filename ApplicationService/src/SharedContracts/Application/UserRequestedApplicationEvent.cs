using static System.Net.Mime.MediaTypeNames;

namespace SharedContracts.Application
{
    public class UserRequestedApplicationEvent
    {
        public string ApplicationId { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateTime RequestedAt { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = default!;
        public string GraduationYear { get; set; } = default!;
        public string Province { get; set; } = default!;
        public string School { get; set; } = default!;
        public string Address { get; set; } = default!;
    }
}
