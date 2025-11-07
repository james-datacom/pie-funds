using MediatR;
using PieFunds.Application.Common.Models;
using PieFunds.Application.Features.Users.Dtos;

namespace PieFunds.Application.Features.Users.Queries.GetUserByEmail
{
    public record GetUserByEmailQuery(string Email) : IRequest<Result<UserDto?>>;
}
