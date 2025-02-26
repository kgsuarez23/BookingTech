using Api.Manager.Application.Entities;
using Api.Manager.Application.Mediator.Queries;
using Api.Manager.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.GestionHotel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestController : Controller
    {
        private readonly IMediator _mediator;

        public GuestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a list of all guests.
        /// </summary>
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("GetAllGuestsQuery")]
        public async Task<ActionResult<Response<GuestDto>>> GetAllGuestsQuery()
        {
            var response = await _mediator.Send(new GetAllGuestsQuery());
            return Ok(response);
        }
    }
}
