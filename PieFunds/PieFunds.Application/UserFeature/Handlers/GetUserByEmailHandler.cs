using MediatR;
using PieFunds.Application.Interfaces;
using PieFunds.Application.UserFeature.Dtos;
using PieFunds.Application.UserFeature.Queries;

namespace PieFunds.Application.UserFeature.Handlers
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
