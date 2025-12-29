using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.CollegeInfoService.GetCampusById
{
    public class GetCampusByIdQueryHandler : IRequestHandler<GetCampusByIdQuery, Campus>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCampusByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Campus> Handle(GetCampusByIdQuery request, CancellationToken cancellationToken)
        {
            var campus = await _unitOfWork.GetRepository<Campus>()
                .GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            if (campus == null) throw new ValidationException("Campus not found");

            //var response = _mapper.Map<ResponseCampus>(campus);
            return campus;
        }
    }
}