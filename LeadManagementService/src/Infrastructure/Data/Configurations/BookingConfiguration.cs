using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.UserFullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.UserEmail)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.UserPhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(b => b.InterestedCampus)
                .HasMaxLength(100);

            builder.Property(b => b.InterestedAcademicField)
                .HasMaxLength(100);

            builder.Property(b => b.InterestedSpecialization)
                .HasMaxLength(100);

            //builder.Property(b => b.InterestedCourse)
            //    .HasMaxLength(100);
            builder.Property(b => b.Location)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Reason)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(b => b.Status)
                .IsRequired()
                .HasConversion<string>(); // store enum as string, optional

            builder.Property(b => b.CreatedByUserId);

            builder.Property(b => b.ClaimedByConsultantId)
                .IsRequired(false);

            builder.Property(b => b.FirstContactAttempt)
                .IsRequired(false);

            builder.Property(b => b.LastContactAttempt)
                .IsRequired(false);

            builder.Property(b => b.ContactStatus)
                .HasConversion<string>()
                .IsRequired()
                .HasDefaultValue(Domain.Enums.ContactStatus.NoContact);

            builder.Property(b => b.ContactAttemptsCount)  // fixed typo here
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(b => b.ClaimedAt);

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.HasMany(b => b.BookingHistories)
                .WithOne(h => h.Booking)
                .HasForeignKey(h => h.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(b => b.ClaimedByConsultantId);
            builder.HasIndex(b => b.Status);
        }
    }
}
