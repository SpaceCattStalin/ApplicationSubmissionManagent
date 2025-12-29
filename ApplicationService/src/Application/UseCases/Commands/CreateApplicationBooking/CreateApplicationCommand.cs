using MediatR;

namespace Application.UseCases.Commands.CreateApplicationBooking
{
    public class CreateApplicationCommand : IRequest<Guid>
    {
        public string UserFullName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string UserPhoneNumber { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = default!;
        public string Province { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string School { get; set; } = default!;
        public string GraduationYear { get; set; } = default!;
        public string Campus { get; set; } = default!;
        public string InterestedAcademicField { get; set; } = default!;
        public string MathScore { get; set; } = default!;
        public string LiteratureScore { get; set; } = default!;
        public string EnglishScore { get; set; } = default!;
    }
}
