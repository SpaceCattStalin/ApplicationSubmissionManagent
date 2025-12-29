using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class BookingDbContextInitialiser
{
    private readonly ILogger<BookingDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public BookingDbContextInitialiser(
        ILogger<BookingDbContextInitialiser> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    //public async Task TrySeedAsync()
    //{
    //    if (_context.Bookings.Any()) return;

    //    var booking1 = new Booking
    //    {
    //        Id = Guid.NewGuid(),
    //        UserFullName = "Alice Johnson",
    //        UserEmail = "alice.johnson@example.com",
    //        UserPhoneNumber = "555-1234",
    //        InterestedCampus = "Main Campus",
    //        InterestedAcademicField = "Computer Science",
    //        InterestedSpecialization = "Artificial Intelligence",
    //        InterestedCourse = "Intro to Programming",
    //        Reason = "Looking to start a career in AI.",
    //        Status = BookingStatus.Waiting,
    //        CreatedByUserId = Guid.NewGuid().ToString(),
    //        ClaimedByConsultantId = null
    //    };

    //    var booking2 = new Booking
    //    {
    //        Id = Guid.NewGuid(),
    //        UserFullName = "Bob Smith",
    //        UserEmail = "bob.smith@example.com",
    //        UserPhoneNumber = "555-5678",
    //        InterestedCampus = "Downtown Campus",
    //        InterestedAcademicField = "Business Administration",
    //        InterestedSpecialization = "Marketing",
    //        InterestedCourse = "Business Fundamentals",
    //        Reason = "Interested in marketing courses.",
    //        Status = BookingStatus.Completed,
    //        CreatedByUserId = Guid.NewGuid().ToString(),
    //        ClaimedByConsultantId = null
    //    };

    //    var bookingHistory1 = new BookingHistory
    //    {
    //        Id = Guid.NewGuid(),
    //        BookingId = booking1.Id,
    //        Content = "Booking created by Alice Johnson",
    //    };

    //    var bookingHistory2 = new BookingHistory
    //    {
    //        Id = Guid.NewGuid(),
    //        BookingId = booking2.Id,
    //        Content = "Booking claimed by consultant",
    //    };

    //    _context.Bookings.AddRange(booking1, booking2);
    //    _context.BookingHistories.AddRange(bookingHistory1, bookingHistory2);

    //    await _context.SaveChangesAsync();
    //}
}
