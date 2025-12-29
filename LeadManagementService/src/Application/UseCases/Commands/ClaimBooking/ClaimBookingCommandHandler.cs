using Application.Common.Interfaces.Identity;
using Application.Common.Interfaces.UnitOfWork;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Commands.ClaimBooking
{
    public class ClaimBookingCommandHandler : IRequestHandler<ClaimBookingCommand, ResponseBooking>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClaimBookingCommandHandler(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseBooking> Handle(ClaimBookingCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Booking>();
            var booking = repository.FirstOrDefault(b => b.Id == request.BookingId);

            if (booking == null)
            {
                throw new KeyNotFoundException($"Booking with ID {request.BookingId} not found.");
            }

            if (booking.Status != BookingStatus.Waiting)
            {
                throw new InvalidOperationException("This booking has already been claimed or is not in a claimable state.");
            }

            var previousStatus = booking.Status;
            booking.ClaimedByConsultantId = _currentUserService.UserId;
            booking.Status = BookingStatus.InProgress;
            booking.ClaimedAt = DateTime.UtcNow;

            var bookingHistory = new BookingHistory
            {
                BookingId = booking.Id,
                Content = "Booking claimed by consultant.",
                PreviousValue = previousStatus.ToString(),
                NewValue = booking.Status.ToString(),
                CreatedAt = DateTime.UtcNow,
                PerformedByUserId = _currentUserService.UserId,
            };

            await _unitOfWork.SaveAsync();

            return _mapper.Map<ResponseBooking>(booking);
        }
    }
}