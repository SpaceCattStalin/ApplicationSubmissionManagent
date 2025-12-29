using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
