using Application.Interfaces;
using Application.UseCases.CollegeInfoService.GetAllCampus;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.CollegeInfoService.GetAllCampus
{
    public class GetAllCampusQueryHandler : IRequestHandler<GetAllCampusQuery, List<CampusDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCampusQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CampusDto>> Handle(GetAllCampusQuery request, CancellationToken cancellationToken)
        {
            var campuses = await _unitOfWork.GetRepository<Campus>()
                .GetAllAsync(cancellationToken: cancellationToken);

            // Chuyển đổi thủ công từ Campus entity sang CampusDto
            var campusDtos = campuses.Select(c => new CampusDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address
            }).ToList();

            return campusDtos;
        }
    }
}