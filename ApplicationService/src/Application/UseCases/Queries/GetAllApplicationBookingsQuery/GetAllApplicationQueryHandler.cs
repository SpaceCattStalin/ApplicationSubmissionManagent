using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using Application.DTOs;
using AutoMapper;
using MediatR;

namespace Application.UseCases.Queries.GetAllApplicationBookingsQuery
{
    public class GetAllApplicationQueryHandler : IRequestHandler<GetAllApplicationQuery, PaginatedList<ResponseApplicationBooking>>
    {
        private readonly IApplicationBookingRepository _applicationRepository;
        private readonly IMapper _mapper;

        public GetAllApplicationQueryHandler(IApplicationBookingRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ResponseApplicationBooking>> Handle(GetAllApplicationQuery request, CancellationToken cancellationToken)
        {
            var filter = new ApplicationFilter
            {
                Id = request.Id,
                Fullname = request.UserFullName,
                Email = request.UserEmail,
                Phone = request.UserPhoneNumber,
                School = request.School,
                GraduationYear = request.GraduationYear,
                Province = request.Province,
                Campus = request.Campus,
                AcademicField = request.InterestedAcademicField,
                ClaimedByConsultantId = request.ClaimedByConsultantId,
                Status = request.Status,
                CreatedAt = request.CreatedAt
            };

            var allResults = await _applicationRepository.SearchAsync(filter);

            var totalCount = allResults.Count();

            var items = allResults
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var responseList = new List<ResponseApplicationBooking>();
            foreach (var item in items)
            {
                var responseItem = _mapper.Map<ResponseApplicationBooking>(item);
                responseList.Add(responseItem);
            }

            return new PaginatedList<ResponseApplicationBooking>(responseList, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
