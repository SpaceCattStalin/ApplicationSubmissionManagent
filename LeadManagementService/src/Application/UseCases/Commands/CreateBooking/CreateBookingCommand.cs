using MediatR;
using Application.DTOs;

namespace Application.UseCases.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<Guid>
    {
        public string UserFullName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string UserPhoneNumber { get; set; } = default!;
        public string? InterestedCampus { get; set; }
        public string? InterestedAcademicField { get; set; }
        public string? InterestedSpecialization { get; set; }
        public string? Location { get; set; }
        public string? Reason { get; set; }
    }
}
