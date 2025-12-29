using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class ApplicationBookingHistoryConfiguration : IEntityTypeConfiguration<ApplicationBookingHistory>
    {
        public void Configure(EntityTypeBuilder<ApplicationBookingHistory> builder)
        {
            builder.HasKey(abh => abh.Id);

            builder.Property(abh => abh.Content)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(abh => abh.CreatedAt)
                .IsRequired();

            builder.Property(abh => abh.PreviousValue)
                .HasMaxLength(1000);

            builder.Property(abh => abh.NewValue)
                .HasMaxLength(1000);

            builder.HasOne(abh => abh.ApplicationBooking)
                .WithMany(ab => ab.Histories)
                .HasForeignKey(abh => abh.ApplicationBookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
