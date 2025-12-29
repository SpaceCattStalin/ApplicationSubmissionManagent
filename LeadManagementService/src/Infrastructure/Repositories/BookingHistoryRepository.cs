using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class BookingHistoryRepository : GenericRepository<BookingHistory>
    {
        public BookingHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
