using Application.UseCases.Commands.ClaimApplicationBooking;
using Application.UseCases.Commands.CreateApplicationBooking;
using Application.UseCases.Queries.GetAllApplicationBookingsQuery;
using Domain.Common;
using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationBookingController : CustomControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationBookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all-applications")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? id = null,
            [FromQuery] string? userFullName = null,
            [FromQuery] string? userEmail = null,
            [FromQuery] string? userPhoneNumber = null,
            [FromQuery] string? province = null,
            [FromQuery] string? school = null,
            [FromQuery] string? graduationYear = null,
            [FromQuery] string? campus = null,
            [FromQuery] Guid? claimedByConsultantId = null,
            [FromQuery] string? interestedAcademicField = null,
            [FromQuery] ApplicationStatus? status = null,
            [FromQuery] DateTime? createdAt = null
        )
        {
            var query = new GetAllApplicationQuery
            {
                Id = id,
                UserFullName = userFullName,
                UserEmail = userEmail,
                UserPhoneNumber = userPhoneNumber,
                Province = province,
                School = school,
                GraduationYear = graduationYear,
                ClaimedByConsultantId = claimedByConsultantId,
                Status = status,
                Campus = campus,
                InterestedAcademicField = interestedAcademicField,
                CreatedAt = createdAt
            };

            var result = await _mediator.Send(query);
            return OkResponse(result);
        }

        [HttpPost("create-application-booking")]
        public async Task<IActionResult> CreateApplicationBooking([FromBody] CreateApplicationCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return OkResponse(result);
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpPost("claim-application-booking")]
        public async Task<IActionResult> ClaimApplication([FromBody] ClaimApplicationCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return OkResponse(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

    }
}
