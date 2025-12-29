using Application.DTOs;
using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IApplicationBookingRepository : IGenericRepository<ApplicationBooking>
    {
        Task<List<ApplicationBooking>> SearchAsync(ApplicationFilter filter);
    }
}
