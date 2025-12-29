using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Campus> Campus => Set<Campus>();

        public DbSet<AcademicField> AcademicFields => Set<AcademicField>();

        public DbSet<Specialization> Specializations => Set<Specialization>();

        public DbSet<Fee> Fees => Set<Fee>();

        public DbSet<CampusAcademicField> CampusAcademicFields => Set<CampusAcademicField>();

        public DbSet<SpecializationFee> SpecializationFees => Set<SpecializationFee>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
