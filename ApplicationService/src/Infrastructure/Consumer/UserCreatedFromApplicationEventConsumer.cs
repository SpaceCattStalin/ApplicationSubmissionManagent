using Application.Common.Interfaces.UnitOfWork;
using Domain.Entities;
using MassTransit;
using SharedContracts.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Consumer
{
    public class UserCreatedFromApplicationEventConsumer : IConsumer<UserCreatedFromApplicationEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserCreatedFromApplicationEventConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Consume(ConsumeContext<UserCreatedFromApplicationEvent> context)
        {
            var userId = context.Message.UserId;
            var email = context.Message.UserEmail;

            var applicationRepository = _unitOfWork.GetRepository<ApplicationBooking>();
            var applicationHistoryRepository = _unitOfWork.GetRepository<ApplicationBookingHistory>();

            var application = await applicationRepository.FirstOrDefaultAsync(a => a.UserEmail == email);
            if (application != null)
            {
                application.CreatedByUserId = userId;
                applicationRepository.Update(application);
            }

            var applicationHistory = await applicationHistoryRepository.FirstOrDefaultAsync(ah => ah.ApplicationBookingId == application.Id);
            if (applicationHistory != null)
            {
                applicationHistory.PerformedByUserId = userId;
                applicationHistoryRepository.Update(applicationHistory);
            }

            await _unitOfWork.SaveAsync();

        }
    }
}
