using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

public class CampusAcademicFieldConfiguration : IEntityTypeConfiguration<CampusAcademicField>
{
    public void Configure(EntityTypeBuilder<CampusAcademicField> builder)
    {
        builder.HasKey(e => new { e.CampusId, e.AcademicFieldId });

        builder.HasOne(e => e.Campus)
               .WithMany(c => c.CampusAcademicFields)
               .HasForeignKey(e => e.CampusId);

        builder.HasOne(e => e.AcademicField)
               .WithMany(a => a.CampusAcademicFields)
               .HasForeignKey(e => e.AcademicFieldId);
    }
}
