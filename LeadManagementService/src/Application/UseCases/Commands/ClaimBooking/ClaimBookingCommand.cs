using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands.ClaimBooking
{
    public class ClaimBookingCommand : IRequest<ResponseBooking>
    {
        public Guid BookingId { get; set; }
        public Guid ConsultantId { get; set; }

    }
}
