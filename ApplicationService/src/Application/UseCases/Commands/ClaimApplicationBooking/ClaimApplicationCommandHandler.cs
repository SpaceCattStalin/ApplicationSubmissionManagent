using Application.Common.Interfaces.Identity;
using Application.Common.Interfaces.UnitOfWork;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using MediatR;

namespace Application.UseCases.Commands.ClaimApplicationBooking
{
    public class ClaimApplicationCommandHandler : IRequestHandler<ClaimApplicationCommand, ResponseApplicationBooking>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClaimApplicationCommandHandler(ICurrentUser currentUser, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseApplicationBooking> Handle(ClaimApplicationCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser.Role != "Consultant")
            {
                throw new UnauthorizedAccessException("Chỉ có Tư vấn viên mới được nhận lịch tư vấn");
            }

            var applicationRepository = _unitOfWork.GetRepository<ApplicationBooking>();
            var application = applicationRepository.FirstOrDefault(a => a.Id == request.ApplicationId);

            var applicationHistoryRepository = _unitOfWork.GetRepository<ApplicationBookingHistory>();

            var previousStatus = application.Status;
            application.ClaimedByConsultantId = _currentUser.UserId;
            application.Status = ApplicationStatus.InProgress;
            application.ClaimedAt = DateTime.UtcNow;

            applicationRepository.Update(application);

            var applicationHistory = new ApplicationBookingHistory
            {
                ApplicationBookingId = application.Id,
                Content = $"Lịch hẹn được nhận bởi tư vấn viên {_currentUser.UserId}.",
                PreviousValue = previousStatus.ToString(),
                NewValue = application.Status.ToString(),
                CreatedAt = DateTime.UtcNow,
                PerformedByUserId = _currentUser.UserId
            };
            applicationHistoryRepository.Add(applicationHistory);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<ResponseApplicationBooking>(application);
        }
    }
}
