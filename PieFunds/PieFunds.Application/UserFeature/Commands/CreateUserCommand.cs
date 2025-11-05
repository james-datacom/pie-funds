using MediatR;
using PieFunds.Application.UserFeature.Dtos;

namespace PieFunds.Application.UserFeature.Commands
{
    public sealed record CreateUserCommand(string Name, string Email) : IRequest<UserDto>;
}