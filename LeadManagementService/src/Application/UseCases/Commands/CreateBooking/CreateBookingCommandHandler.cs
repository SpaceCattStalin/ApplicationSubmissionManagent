using Application.Common.Interfaces.Identity;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Service;
using Application.Common.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MassTransit;
using MediatR;
using SharedContracts.Booking;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Guid>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailValidator _emailValidator;

        public CreateBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPublishEndpoint publishEndpoint, IEmailValidator emailValidator)
        {
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailValidator = emailValidator;
        }

        public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {

            // Validate email format
            if (string.IsNullOrWhiteSpace(request.UserEmail) ||
                 !new EmailAddressAttribute().IsValid(request.UserEmail))
            {
                throw new ValidationException("Invalid email format.");
            }

            // Validate email domain
            if (!await _emailValidator.HasValidMxRecordAsync(request.UserEmail))
            {
                throw new ValidationException("Email domain does not have a valid MX record.");
            }

            var booking = _mapper.Map<Booking>(request);

            var bookingRepository = _unitOfWork.GetRepository<Booking>();

            var hasWaitingBooking = await bookingRepository.FirstOrDefaultAsync(b =>
                b.UserEmail == request.UserEmail &&
                b.UserFullName == request.UserFullName &&
                b.UserPhoneNumber == request.UserPhoneNumber &&
                b.Status == BookingStatus.Waiting
            );

            if (hasWaitingBooking != null)
            {
                throw new InvalidOperationException("Bạn đã có một lịch tư vấn.");
            }

            bookingRepository.Add(booking);
            await _unitOfWork.SaveAsync();

            var bookingHistoryRepository = _unitOfWork.GetRepository<BookingHistory>();
            var bookingHistory = new BookingHistory
            {
                BookingId = booking.Id,
                Content = $"Lịch hẹn tư vấn tạo cho người dùng {request.UserFullName}",
                CreatedAt = DateTime.UtcNow,
                PreviousValue = null,
                NewValue = booking.ContactStatus.ToString(),
            };
            bookingHistoryRepository.Add(bookingHistory);
            await _unitOfWork.SaveAsync();

            var @event = new UserRequestedBookingEvent
            {
                Email = request.UserEmail,
                FullName = request.UserFullName,
                PhoneNumber = request.UserPhoneNumber,
                RequestedAt = DateTime.UtcNow
            };

            Console.WriteLine($"Publishing event: {@event.FullName} requested a booking at {DateTime.UtcNow}");
            await _publishEndpoint.Publish(@event);
            Console.WriteLine("Event published!");


            return await Task.FromResult(booking.Id);
        }
    }
}
