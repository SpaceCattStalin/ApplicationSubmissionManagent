using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.UseCases.CollegeInfoService.GetAllCampus
{
    public class GetAllCampusQuery : IRequest<List<CampusDto>>
    {
        // Không cần tham số, chỉ lấy tất cả
    }

    // DTO tương tự ResponseCampus nhưng không dùng AutoMapper
    public class CampusDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        // Thêm các trường khác nếu cần, khớp với Domain.Entities.Campus

        public string Description { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        public List<string> AcademicFields { get; set; }

        public List<SpecializationDto> Specializations { get; set; }
    }

    public class SpecializationDto
    {
        //public CampusAcademicField CampusAcademicField { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class CampusAcademicField
    {
        public Guid CampusId { get; set; }
        public Guid AcademicFieldId { get; set; }

        public Campus Campus { get; set; }
        public AcademicField AcademicField { get; set; }

        public ICollection<Specialization> Specializations { get; set; }
    }

}