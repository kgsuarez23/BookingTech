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
    public class RoomController : Controller
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all room records.
        /// </summary>
        [HttpGet("GetAllQuery")]
        public async Task<ActionResult<Response<RoomDto>>> GetAllQuery()
        {
            var response = await _mediator.Send(new GetAllRoomsQuery());
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all types of rooms available.
        /// </summary>
        [HttpGet("GetAllTypeRooms")]
        public async Task<ActionResult<Response<TypeRoomDto>>> GetAllTypeRooms()
        {
            var response = await _mediator.Send(new GetAllTypeRooms());
            return Ok(response);
        }

        /// <summary>
        /// Retrieves details of a room by its identifier.
        /// </summary>
        [HttpGet("GetByIdQuery")]
        public async Task<ActionResult<Response<RoomDto>>> GetByIdQuery([FromQuery] GetByIdRoomQuery req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }

        /// <summary>
        /// Finds rooms that match specific filter criteria.
        /// </summary>
        [HttpGet("FindRoomsByFilterQuery")]
        public async Task<ActionResult<Response<RoomFilterDto>>> FindRoomsByFilterQuery([FromQuery] FindRoomsByFilterQuery req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new room record.
        /// </summary>
        [HttpPost("CreateRoomCommand")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Response<RoomDto>>> CreateRoomCommand([FromBody] CreateRoomCommand req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing room record.
        /// </summary>
        [HttpPost("UpdateRoomCommand")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Response<RoomDto>>> UpdateRoomCommand([FromBody] UpdateRoomCommand req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }

        /// <summary>
        /// Updates the state of an existing room.
        /// </summary>
        [HttpPatch("UpdateStateRoomCommand")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Response<RoomDto>>> UpdateStateRoomCommand([FromBody] UpdateStateRoomCommand req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }
    }
}
