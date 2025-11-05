using MediatR;
using PieFunds.Application.Interfaces;
using PieFunds.Application.UserFeature.Commands;
using PieFunds.Application.UserFeature.Dtos;
using PieFunds.Domain.Entities;
using PieFunds.Application.UserFeature.Exceptions;

namespace PieFunds.Application.UserFeature.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                throw new DuplicateEmailException(request.Email);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email
            };

            await _userRepository.AddAsync(user, cancellationToken);

            return new UserDto(user.Id, user.Name, user.Email);
        }
    }
}