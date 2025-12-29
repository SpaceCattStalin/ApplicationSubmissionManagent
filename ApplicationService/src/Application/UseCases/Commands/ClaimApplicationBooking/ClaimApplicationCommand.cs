using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands.ClaimApplicationBooking
{
    public class ClaimApplicationCommand : IRequest<ResponseApplicationBooking>
    {
        public Guid ApplicationId { get; set; }
    }
}
