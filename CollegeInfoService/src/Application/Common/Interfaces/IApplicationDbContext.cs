
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Campus> Campus { get; }
        DbSet<AcademicField> AcademicFields { get; }
        DbSet<Specialization> Specializations { get; }
        DbSet<CampusAcademicField> CampusAcademicFields { get; }
        DbSet<Fee> Fees { get; }

        DbSet<SpecializationFee> SpecializationFees { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
