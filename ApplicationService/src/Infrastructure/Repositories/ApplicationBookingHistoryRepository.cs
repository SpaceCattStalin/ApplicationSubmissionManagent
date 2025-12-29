using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ApplicationBookingHistoryRepository : GenericRepository<ApplicationBookingHistory>
    {
        public ApplicationBookingHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
