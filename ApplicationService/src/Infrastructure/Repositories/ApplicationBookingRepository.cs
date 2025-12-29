using Application.Common.Interfaces.Repositories;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ApplicationBookingRepository : GenericRepository<ApplicationBooking>, IApplicationBookingRepository
    {
        public ApplicationBookingRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<ApplicationBooking>> SearchAsync(ApplicationFilter filter)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Id))
            {
                if (Guid.TryParse(filter.Id, out var id))
                {
                    query = query.Where(a => a.Id == id);
                }
            }

            if (!string.IsNullOrEmpty(filter.Fullname))
            {
                query = query.Where(a => a.UserFullName.Contains(filter.Fullname));
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(a => a.UserEmail.Contains(filter.Email));
            }

            if (!string.IsNullOrEmpty(filter.BirthDate))
            {
                if (DateTime.TryParse(filter.BirthDate, out var birthDate))
                {
                    query = query.Where(a => a.DateOfBirth.Date == birthDate.Date);
                }
            }

            if (!string.IsNullOrEmpty(filter.Phone))
            {
                query = query.Where(a => a.UserPhoneNumber.Contains(filter.Phone));
            }

            if (!string.IsNullOrEmpty(filter.School))
            {
                query = query.Where(a => a.School.Contains(filter.School));
            }

            if (!string.IsNullOrEmpty(filter.GraduationYear))
            {
                if (int.TryParse(filter.GraduationYear, out var year))
                {
                    query = query.Where(a => a.GraduationYear == year);
                }
            }

            if (!string.IsNullOrEmpty(filter.Province))
            {
                query = query.Where(a => a.Province.Contains(filter.Province));
            }

            if (!string.IsNullOrEmpty(filter.Campus))
            {
                query = query.Where(a => a.Campus.Contains(filter.Campus));
            }

            if (!string.IsNullOrEmpty(filter.AcademicField))
            {
                query = query.Where(a => a.InterestedAcademicField.Contains(filter.AcademicField));
            }

            if (!string.IsNullOrEmpty(filter.ClaimedByConsultantId.ToString()))
            {
                query = query.Where(a => a.ClaimedByConsultantId == filter.ClaimedByConsultantId);
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(a => a.Status == filter.Status);
            }

            if (filter.CreatedAt.HasValue)
            {
                query = query.Where(a => a.CreatedAt.Date == filter.CreatedAt.Value.Date);
            }

            return await query.OrderByDescending(a => a.CreatedAt).ToListAsync();
        }
    }
}
