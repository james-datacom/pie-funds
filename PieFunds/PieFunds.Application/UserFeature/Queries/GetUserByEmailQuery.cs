using MediatR;
using PieFunds.Application.UserFeature.Dtos;

namespace PieFunds.Application.UserFeature.Queries
{
    public sealed record GetUserByEmailQuery(string Email) : IRequest<UserDto?>;
}
