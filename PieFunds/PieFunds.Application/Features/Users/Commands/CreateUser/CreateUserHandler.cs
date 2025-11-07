using MediatR;
using PieFunds.Application.Features.Users.Dtos;
using PieFunds.Application.Interfaces;
using PieFunds.Domain.Entities;

namespace PieFunds.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                return new CreateUserResult(false, "Email already registered.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email
            };

            await _userRepository.AddAsync(user, cancellationToken);

            var userDto = new UserDto(user.Id, user.Name, user.Email);
            return new CreateUserResult(true, "User created successfully.", userDto);
        }
    }
}