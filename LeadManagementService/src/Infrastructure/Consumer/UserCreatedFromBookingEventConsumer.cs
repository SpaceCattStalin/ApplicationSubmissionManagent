using Application.Common.Interfaces.UnitOfWork;
using Domain.Entities;
using MassTransit;
using SharedContracts.Booking;

namespace Infrastructure.Consumer
{
    public class UserCreatedFromBookingEventConsumer : IConsumer<UserCreatedFromBookingEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserCreatedFromBookingEventConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Consume(ConsumeContext<UserCreatedFromBookingEvent> context)
        {
            var email = context.Message.UserEmail;
            var userId = context.Message.UserId;

            var bookingRepository = _unitOfWork.GetRepository<Booking>();
            var bookingHistoryRepository = _unitOfWork.GetRepository<BookingHistory>();

            var booking = await bookingRepository.FirstOrDefaultAsync(b => b.UserEmail == email);
            if (booking != null)
            {
                booking.CreatedByUserId = userId;
                bookingRepository.Update(booking);
            }

            var bookingHistory = await bookingHistoryRepository.FirstOrDefaultAsync(bh => bh.BookingId == booking.Id);
            if (bookingHistory != null)
            {
                bookingHistory.PerformedByUserId = userId;
                bookingHistoryRepository.Update(bookingHistory);
            }

            await _unitOfWork.SaveAsync();
        }
    }
}
