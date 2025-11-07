using MediatR;
using Microsoft.AspNetCore.Mvc;
using PieFunds.Application.Features.Users.Commands.CreateUser;
using PieFunds.Application.Features.Users.Queries.GetUserByEmail;


namespace PieFunds.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {

        /// <summary>
        /// Gets a user by email.
        /// </summary>
        /// <param email="email">The email of the user.</param>
        [HttpGet("{email}", Name = "GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            var result = await mediator.Send(new GetUserByEmailQuery(email));
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await mediator.Send(command);
            if (result == null || !result.Success) {
                string message = result?.Message != null ? "Email already exists." : "User could not be created.";
                return BadRequest(message);
            }
            return Ok(result);
        }
    }
}
