using Application.Common.Interfaces.UnitOfWork;
using Domain.Entities;
using MassTransit;
using SharedContracts.User;

namespace Infrastructure.Consumer
{
    public class UserAlreadyExistApplicationConsumer : IConsumer<UserAlreadyExistApplicationEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint;
        public UserAlreadyExistApplicationConsumer(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<UserAlreadyExistApplicationEvent> context)
        {
            var email = context.Message.UserEmail;
            var userId = context.Message.UserId;
            var applicationId = context.Message.ApplicationId;

            var applicationRepository = _unitOfWork.GetRepository<ApplicationBooking>();
            var applicationHistoryRepository = _unitOfWork.GetRepository<ApplicationBookingHistory>();

            var existingApplication = await applicationRepository.FirstOrDefaultAsync(a => a.Id == Guid.Parse(applicationId) && a.CreatedByUserId == Guid.Empty);

            if (existingApplication != null)
            {
                existingApplication.CreatedByUserId = userId;
                applicationRepository.Update(existingApplication);
            }
            var applicationHistory = await applicationHistoryRepository.FirstOrDefaultAsync(ah => ah.ApplicationBookingId == existingApplication.Id);
            if (applicationHistory != null)
            {
                applicationHistory.PerformedByUserId = userId;
                applicationHistoryRepository.Update(applicationHistory);
            }

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving application: {ex.Message}");
            }

            var @event = new CheckUserBookingEvent
            {
                UserEmail = email,
                UserId = userId
            };
            await _publishEndpoint.Publish(@event);
        }
    }
}
