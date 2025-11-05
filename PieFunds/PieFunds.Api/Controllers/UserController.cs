using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PieFunds.Application.UserFeature.Commands;
using PieFunds.Application.UserFeature.Exceptions;
using PieFunds.Application.UserFeature.Queries;

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
            try
            {
                var result = await mediator.Send(command);
                if (result == null)
                    return BadRequest("User could not be created.");
                if (string.IsNullOrWhiteSpace(result?.Email))
                    return BadRequest("User email is required for location header.");
                return CreatedAtRoute("GetUserByEmail", new { email = result.Email }, result);
            }
            catch (DuplicateEmailException)
            {
                return Conflict("A user with this email already exists.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
