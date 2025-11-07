using PieFunds.Application.Features.Users.Dtos;

namespace PieFunds.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserResult
    {
        public bool Success { get; }
        public string Message { get; }
        public UserDto? User { get; }

        public CreateUserResult(bool success, string message, UserDto? user = null)
        {
            Success = success;
            Message = message;
            User = user;
        }
    }
}