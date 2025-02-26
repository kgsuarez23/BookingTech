using Api.Manager.Application.Entities;
using Api.Manager.Application.Mediator.Commands;
using Api.Manager.Application.Mediator.Queries;
using Api.Manager.Application.Wrappers;
using Api.Manager.Base.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.GestionHotel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag(description: "Controller responsible for handling authentication and user management endpoints.")]
    public class AuthLoginController : Controller
    {
        private readonly IMediator _mediator;

        public AuthLoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Processes a login request using the provided login query.
        /// </summary>
        /// <param name="req">The login query containing user credentials passed as query parameters.</param>
        [HttpGet("LoginQuery")]
        public async Task<ActionResult<TokenLogin>> LoginQuery([FromQuery] LoginQuery req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all user roles.
        /// This endpoint is secured and requires admin privileges.
        /// </summary>
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("GetAllRoleUserQuery")]
        public async Task<ActionResult<Response<RoleDto>>> GetAllRoleUserQuery()
        {
            var response = await _mediator.Send(new GetAllRoleUserQuery());
            return Ok(response);
        }

        /// <summary>
        /// Adds a role to a user.
        /// This endpoint is secured and requires admin privileges.
        /// </summary>
        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("AddRoleToUserCommand")]
        public async Task<ActionResult<BasicResult>> AddRoleToUserCommand([FromBody] AddRoleToUserCommand req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing role for a user.
        /// This endpoint is secured and requires admin privileges.
        /// </summary>
        [Authorize(Policy = "AdminPolicy")]
        [HttpPatch("UpdateRoleToUserCommand")]
        public async Task<ActionResult<BasicResult>> UpdateRoleToUserCommand([FromBody] UpdateRoleToUserCommand req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        [HttpPost("CreateUserCommand")]
        public async Task<ActionResult<BasicResult>> CreateUserCommand([FromBody] CreateUserCommand req)
        {
            var response = await _mediator.Send(req);
            return Ok(response);
        }
    }
}
