using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.CollegeInfoService.GetCampusById
{
    public record GetCampusByIdQuery : IRequest<Campus>
    {
        public Guid Id { get; init; } = default!;
    }
}