using Application.UseCases.CollegeInfoService.GetAllCampus;
using MediatR;

public class GetCampusByNameQuery : IRequest<CampusDto>
{
    public string Name { get; set; }
}
