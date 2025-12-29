using Application.Common.Models;
using Application.DTOs;
using MediatR;

namespace Application.UseCases.Queries.GetAllBookingsQuery
{
    public class GetAllBookingQuery : IRequest<PaginatedList<ResponseBooking>>
    {
        public string? UserFullName { get; set; }
        public string? UserEmail { get; set; } = default!;
        public string? UserPhoneNumber { get; set; }
        public string? InterestedCampus { get; set; }
        public string? InterestedAcademicField { get; set; }
        public string? InterestedSpecialization { get; set; }
        //public string? InterestedCourse { get; set; }
        public string? Location { get; set; } = default!;
        public string? Status { get; set; } = default!;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
