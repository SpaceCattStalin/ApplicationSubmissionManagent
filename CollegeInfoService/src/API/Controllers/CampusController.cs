using Application.UseCases.CollegeInfoService.GetAllCampus;
using Application.UseCases.CollegeInfoService.GetCampusById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    [ApiController]
    [Route("[controller]")]
    //[Route("collegeinfo/[controller]")]

    public class CampusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampusById(Guid id)
        {
            var query = new GetCampusByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCampus()
        {
            var query = new GetAllCampusQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        //[HttpGet("hanoi")]
        //public async Task<IActionResult> GetHanoiCampus()
        //{
        //    var query = new GetCampusByNameQuery { Name = "Hà Nội Campus" };
        //    var result = await _mediator.Send(query);
        //    if (result == null)
        //        return NotFound("Không tìm thấy campus Hà Nội.");

        //    return Ok(result);
        //}

        [HttpGet("hanoi")]
        public async Task<IActionResult> GetHanoiCampus()
        {
            var query = new GetCampusByNameQuery { Name = "Hà Nội Campus" };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound("Không tìm thấy campus Hà Nội.");
        }

        [HttpGet("danang")]
        public async Task<IActionResult> GetDaNangCampus()
        {
            var query = new GetCampusByNameQuery { Name = "Đà Nẵng Campus" };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound("Không tìm thấy campus Đà Nẵng.");
        }

        [HttpGet("cantho")]
        public async Task<IActionResult> GetCanThoCampus()
        {
            var query = new GetCampusByNameQuery { Name = "Cần Thơ Campus" };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound("Không tìm thấy campus Cần Thơ.");
        }

        [HttpGet("hcm")]
        public async Task<IActionResult> GetHCMCampus()
        {
            var query = new GetCampusByNameQuery { Name = "Hồ Chí Minh Campus" };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound("Không tìm thấy campus Hồ Chí Minh.");
        }

        [HttpGet("quynhon")]
        public async Task<IActionResult> GetQuyNhonCampus()
        {
            var query = new GetCampusByNameQuery { Name = "Quy Nhơn Campus" };
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound("Không tìm thấy campus Quy Nhơn.");
        }



    }
}