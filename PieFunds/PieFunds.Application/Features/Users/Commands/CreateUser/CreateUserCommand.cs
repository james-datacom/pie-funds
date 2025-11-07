using MediatR;
using PieFunds.Application.Features.Users.Dtos;

namespace PieFunds.Application.Features.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(string Name, string Email) : IRequest<CreateUserResult>;
}