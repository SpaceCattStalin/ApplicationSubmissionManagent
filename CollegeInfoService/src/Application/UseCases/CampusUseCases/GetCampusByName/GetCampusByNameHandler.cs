using Application.Common.Interfaces;
using Application.UseCases.CollegeInfoService.GetAllCampus;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetCampusByNameHandler : IRequestHandler<GetCampusByNameQuery, CampusDto>
{
    private readonly IApplicationDbContext _context;

    public GetCampusByNameHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CampusDto> Handle(GetCampusByNameQuery request, CancellationToken cancellationToken)
    {
        var campus = await _context.Campus
            .Where(c => c.Name == request.Name)
            .Select(c => new CampusDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Description = c.Description,
                ContactPhone = c.ContactPhone,
                ContactEmail = c.ContactEmail,

                AcademicFields = _context.CampusAcademicFields
                        .Where(caf => caf.CampusId == c.Id)
                        .Select(caf => caf.AcademicField.Name)
                        .ToList(),

                Specializations = _context.Specializations
                        .Where(s => s.CampusAcademicField.CampusId == c.Id)
                        .Select(s => new SpecializationDto
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Description = s.Description,
                            IsActive = s.IsActive
                        })
                        .ToList()



            })
            .FirstOrDefaultAsync(cancellationToken);

        return campus;
    }
}
