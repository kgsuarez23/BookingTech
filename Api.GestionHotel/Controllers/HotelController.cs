using Api.Manager.Application.Entities;
using Api.Manager.Application.Mediator.Commands;
using Api.Manager.Application.Mediator.Queries;
using Api.Manager.Application.Wrappers;
using Api.Manager.Base.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.GestionHotel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : Controller
    {

        private readonly IMediator _mediator;

        public HotelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a list of all hotels.
        /// </summary>
        [HttpGet("GetAllHotelsQuery")]
        public async Task<ActionResult<Response<HotelDto>>> GetAllHotelsQuery()
        {
            var response = await _mediator.Send(new GetAllHotelsQuery());
            return Ok(response);
        }

        /// <summary>
        /// Retrieves the details of a hotel by its identifier.
        /// </summary>
        [HttpGet("GetByIdHotelQuery")]
        public async Task<ActionResult<Response<HotelDto>>> GetByIdHotelQuery([FromQuery] GetByIdHotelQuery req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new hotel record.
        /// </summary>
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("CreateHotelCommand")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<BasicResult>> CreateHotelCommand([FromBody] CreateHotelCommand req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }

        /// <summary>
        /// Updates the details of an existing hotel.
        /// </summary>
        [Authorize(Policy = "AdminPolicy")]
        [HttpPatch("UpdateHotelCommand")]
        public async Task<ActionResult<Response<HotelDto>>> UpdateHotelCommand([FromBody] UpdateHotelCommand req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }

        /// <summary>
        /// Updates the state of an existing hotel.
        /// </summary>
        [Authorize(Policy = "AdminPolicy")]
        [HttpPatch("UpdateStateHotelCommand")]
        public async Task<ActionResult<Response<HotelDto>>> UpdateStateHotelCommand([FromBody] UpdateStateHotelCommand req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }
    }
}
