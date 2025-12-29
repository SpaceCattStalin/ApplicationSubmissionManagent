using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class SpecializationFeeConfiguration : IEntityTypeConfiguration<SpecializationFee>
    {
        public void Configure(EntityTypeBuilder<SpecializationFee> builder)
        {
            builder.HasKey(sf => new { sf.SpecializationId, sf.FeeId });

            builder.HasOne(sf => sf.Specialization)
                   .WithMany(s => s.SpecializationFees)
                   .HasForeignKey(sf => sf.SpecializationId);

            builder.HasOne(sf => sf.Fee)
                   .WithMany(f => f.SpecializationFees)
                   .HasForeignKey(sf => sf.FeeId);
        }
    }
}
