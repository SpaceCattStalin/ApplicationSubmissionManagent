using Application.Common.Interfaces.UnitOfWork;
using Domain.Entities;
using MassTransit;
using SharedContracts.Booking;
using SharedContracts.Notification;

namespace Infrastructure.Consumer
{
    public class UserWithClaimedBookingEventConsumer : IConsumer<UserWithClaimedBookingEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _publishEndpoint;
        public UserWithClaimedBookingEventConsumer(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
        {
            _unitOfWork = unitOfWork;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<UserWithClaimedBookingEvent> context)
        {
            var consultantId = context.Message.ConsultantId;
            var userId = context.Message.UserId;

            var applicationRepository = _unitOfWork.GetRepository<ApplicationBooking>();

            var existingApplication = applicationRepository.FirstOrDefault(a => a.CreatedByUserId == userId);
            if (existingApplication != null)
            {
                existingApplication.ClaimedByConsultantId = consultantId;
                applicationRepository.Update(existingApplication);

                await _publishEndpoint.Publish(new ConsultantAssignedApplicationNotificationEvent
                {
                    ConsultantId = consultantId,
                    ApplicationId = existingApplication.Id,
                    ApplicantName = existingApplication.UserFullName
                });
            }

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving application: {ex.Message}");
            }
        }
    }
}
