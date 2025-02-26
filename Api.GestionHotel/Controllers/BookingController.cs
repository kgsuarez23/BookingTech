using Api.Manager.Application.Entities;
using Api.Manager.Application.Mediator.Commands;
using Api.Manager.Application.Mediator.Queries;
using Api.Manager.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.GestionHotel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all booking records.
        /// </summary>
        [Authorize(Policy = "UserPolicy")]
        [HttpGet("GetAllBookingsQuery")]
        public async Task<ActionResult<Response<BookingDto>>> GetAllBookingsQuery()
        {
            var response = await _mediator.Send(new GetAllBookingsQuery());
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a booking record by its identifier.
        /// </summary>
        [Authorize(Policy = "UserPolicy")]
        [HttpGet("GetByIdBookingsQuery")]
        public async Task<ActionResult<Response<BookingDto>>> GetByIdBookingsQuery([FromQuery] GetByIdBookingsQuery req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new booking.
        /// </summary>
        [Authorize(Policy = "UserPolicy")]
        [HttpPost("CreateBookingCommand")]
        public async Task<ActionResult<Response<BookingDto>>> CreateBookingCommand([FromBody] CreateBookingCommand req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }
    }
}
