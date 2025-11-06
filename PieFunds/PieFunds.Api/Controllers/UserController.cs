using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            var result = await mediator.Send(new GetUserByEmailQuery(email));
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }
    }
}
