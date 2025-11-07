using MediatR;
using PieFunds.Application.Common.Models;
using PieFunds.Application.Constants;
using PieFunds.Application.Features.Users.Dtos;
using PieFunds.Application.Interfaces;

namespace PieFunds.Application.Features.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, Result<UserDto?>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserDto?>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user == null)
            {
                return Result<UserDto?>.Fail(ErrorCodes.UserNotFound, $"User with email {request.Email} not found");
            }

            var dto = new UserDto(user.Id, user.Name, user.Email);

            return Result<UserDto?>.Ok(dto);
        }


    }

}
