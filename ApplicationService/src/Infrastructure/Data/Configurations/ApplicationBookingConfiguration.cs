using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ApplicationBookingConfiguration : IEntityTypeConfiguration<ApplicationBooking>
    {
        public void Configure(EntityTypeBuilder<ApplicationBooking> builder)
        {
            builder.HasKey(ab => ab.Id);

            builder.Property(ab => ab.CreatedByUserId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ab => ab.ClaimedByConsultantId)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(ab => ab.UserFullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ab => ab.UserEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ab => ab.UserPhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(ab => ab.Province)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ab => ab.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(ab => ab.School)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(ab => ab.Campus)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ab => ab.InterestedAcademicField)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ab => ab.DateOfBirth)
                .IsRequired();

            builder.Property(ab => ab.GraduationYear)
                .IsRequired();

            builder.Property(ab => ab.MathScore)
                .IsRequired();

            builder.Property(ab => ab.EnglishScore)
                .IsRequired();

            builder.Property(ab => ab.LiteratureScore)
                .IsRequired();

            builder.Property(ab => ab.CreatedAt)
              .IsRequired()
              .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ab => ab.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), v))
                .HasDefaultValue(ApplicationStatus.Waiting);

            // Navigation property configuration for Histories
            builder.HasMany(ab => ab.Histories)
                .WithOne()
                .HasForeignKey(h => h.ApplicationBookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
