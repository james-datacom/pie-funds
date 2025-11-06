using MediatR;
using PieFunds.Application.Features.Users.Dtos;
using PieFunds.Application.Interfaces;

namespace PieFunds.Application.Features.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserDto?>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user == null)
            {
                return null;
            }

            return new UserDto(user.Id, user.Name, user.Email);
        }


    }

}
