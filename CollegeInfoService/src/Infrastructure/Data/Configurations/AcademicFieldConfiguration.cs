using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

public class AcademicFieldConfiguration : IEntityTypeConfiguration<AcademicField>
{
    public void Configure(EntityTypeBuilder<AcademicField> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired();
    }
}
