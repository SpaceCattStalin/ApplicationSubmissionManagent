using Application.Common.Interfaces.UnitOfWork;
using Domain.Entities;
using MassTransit;
using SharedContracts.User;

namespace Infrastructure.Consumer
{
    public class UserAlreadyExistConsumer : IConsumer<UserAlreadyExistsEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserAlreadyExistConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<UserAlreadyExistsEvent> context)
        {
            var email = context.Message.UserEmail;
            var userId = context.Message.UserId;

            var bookingRepository = _unitOfWork.GetRepository<Booking>();
            var bookingHistoryRepository = _unitOfWork.GetRepository<BookingHistory>();

            var booking = await bookingRepository.FirstOrDefaultAsync(b => b.UserEmail == email && b.CreatedByUserId == null);
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

            try
            {
                Console.WriteLine($"Booking ID: {booking.Id}, UserEmail: {booking.UserEmail}, CreatedByUserId: {booking.CreatedByUserId}");

                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while processing the user already exists event.", ex);
            }
        }
    }
}
