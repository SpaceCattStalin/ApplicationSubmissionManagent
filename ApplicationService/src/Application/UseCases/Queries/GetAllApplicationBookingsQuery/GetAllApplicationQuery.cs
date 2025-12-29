using Application.Common.Models;
using Application.DTOs;
using Domain.Enum;
using MediatR;

namespace Application.UseCases.Queries.GetAllApplicationBookingsQuery
{
    public class GetAllApplicationQuery : IRequest<PaginatedList<ResponseApplicationBooking>>
    {
        public string? Id { get; set; }
        public string? UserFullName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public string? Province { get; set; }
        public string? School { get; set; }
        public string? GraduationYear { get; set; }
        public string? Campus { get; set; }
        public string? InterestedAcademicField { get; set; }
        public Guid? ClaimedByConsultantId { get; set; }
        public ApplicationStatus? Status { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
