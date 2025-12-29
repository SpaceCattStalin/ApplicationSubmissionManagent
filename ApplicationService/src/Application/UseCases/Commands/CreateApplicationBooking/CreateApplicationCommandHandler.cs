
using Application.Common.Interfaces.Service;
using Application.Common.Interfaces.UnitOfWork;
using AutoMapper;
using MassTransit;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using SharedContracts.Application;

namespace Application.UseCases.Commands.CreateApplicationBooking
{
    public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, Guid>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailValidator _emailValidator;
        public CreateApplicationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailValidator emailValidator, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailValidator = emailValidator;
        }
        public async Task<Guid> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {

            // Validate email format
            if (string.IsNullOrWhiteSpace(request.UserEmail) ||
                 !new EmailAddressAttribute().IsValid(request.UserEmail))
            {
                throw new ValidationException("Địa chỉ email không đúng định dạng");
            }

            // Validate email domain
            if (!await _emailValidator.HasValidMxRecordAsync(request.UserEmail))
            {
                throw new ValidationException("Tên miền email không tồn tại hoặc không thể nhận email");
            }
            var applicationRepository = _unitOfWork.GetRepository<ApplicationBooking>();

            var application = _mapper.Map<ApplicationBooking>(request);

            applicationRepository.Add(application);

            var applicationHistoryRepository = _unitOfWork.GetRepository<ApplicationBookingHistory>();
            var applicationHistory = new ApplicationBookingHistory
            {
                ApplicationBookingId = application.Id,
                Content = $"Hồ sơ xét tuyển tạo cho người dùng {request.UserFullName}",
                PerformedByUserId = application.CreatedByUserId,
                CreatedAt = DateTime.UtcNow,
                PreviousValue = null,
                NewValue = application.Status.ToString()
            };
            applicationHistoryRepository.Add(applicationHistory);

            await _unitOfWork.SaveAsync();

            var @event = new UserRequestedApplicationEvent
            {
                ApplicationId = application.Id.ToString(),
                Address = request.Address,
                BirthDate = request.BirthDate,
                Email = request.UserEmail,
                FullName = request.UserFullName,
                Gender = request.Gender,
                GraduationYear = request.GraduationYear,
                PhoneNumber = request.UserPhoneNumber,
                Province = request.Province,
                School = request.School,
                RequestedAt = DateTime.UtcNow
            };

            Console.WriteLine($"Publishing event: {@event.GetType().Name} for user {request.UserFullName} with email {request.UserEmail}");
            await _publishEndpoint.Publish(@event);
            Console.WriteLine("Event published!");

            return await Task.FromResult(application.Id);
        }
    }
}
