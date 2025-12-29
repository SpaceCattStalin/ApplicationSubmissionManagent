using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Models;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Queries.GetAllBookingsQuery
{
    public class GetAllBookingQueryHandler : IRequestHandler<GetAllBookingQuery, PaginatedList<ResponseBooking>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBookingQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedList<ResponseBooking>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.GetRepository<Booking>().Entities;

            var query = repository
                .Where(b => (string.IsNullOrEmpty(request.UserFullName) || b.UserFullName.Contains(request.UserFullName))
                            && (string.IsNullOrEmpty(request.UserEmail) || b.UserEmail == request.UserEmail)
                            && (string.IsNullOrEmpty(request.UserPhoneNumber) || b.UserPhoneNumber == request.UserPhoneNumber)
                            && (string.IsNullOrEmpty(request.InterestedCampus) || b.InterestedCampus == request.InterestedCampus)
                            && (string.IsNullOrEmpty(request.InterestedAcademicField) || b.InterestedAcademicField == request.InterestedAcademicField)
                            && (string.IsNullOrEmpty(request.InterestedSpecialization) || b.InterestedSpecialization == request.InterestedSpecialization)
                            && (string.IsNullOrEmpty(request.Location) || b.Location.Contains(request.Location))
                            && (string.IsNullOrEmpty(request.Status) || b.Status.ToString() == request.Status))
                .OrderByDescending(b => b.CreatedAt);

            var totalCount = query.Count();

            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var responseList = new List<ResponseBooking>();
            foreach (var item in items)
            {
                var responseBooking = _mapper.Map<ResponseBooking>(item);
                responseList.Add(responseBooking);
            }

            return new PaginatedList<ResponseBooking>(responseList, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
