using Application.UseCases.Commands.CreateBooking;
using Domain.Events;
using MassTransit;
using MediatR;
using SharedContracts.Booking;

namespace Infrastructure.Consumer
{
    public class CreateBookingConsumer : IConsumer<CreateBookingEvent>
    {
        private readonly IMediator _mediator;

        public CreateBookingConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreateBookingEvent> context)
        {
            var message = context.Message;

            // Send MediatR command
            var bookingId = await _mediator.Send(new CreateBookingCommand
            {
                UserEmail = message.UserEmail,
                UserFullName = message.UserFullName,
                UserPhoneNumber = message.UserPhoneNumber
            });

            // Now publish a public event – this will use the Outbox
            await context.Publish(new UserRequestedBookingEvent
            {
                Email = message.UserEmail,
                FullName = message.UserFullName,
                PhoneNumber = message.UserPhoneNumber,
                RequestedAt = DateTime.UtcNow
            });

            Console.WriteLine($"Booking created and event published via outbox for: {message.UserEmail}");
        }
    }
}
