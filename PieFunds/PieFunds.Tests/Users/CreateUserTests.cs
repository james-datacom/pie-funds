using System;
using System.Threading.Tasks;
using MediatR;
using PieFunds.Application.UserFeature.Commands;
using PieFunds.Application.UserFeature.Queries;
using PieFunds.Tests.TestInfrastructure;
using PieFunds.Application.UserFeature.Exceptions;
using Xunit;

namespace PieFunds.Tests.Users
{
    public class CreateUserTests : IClassFixture<MediatorFixture>
    {
        private readonly IMediator _mediator;

        public CreateUserTests(MediatorFixture fixture)
        {
            _mediator = fixture.Mediator;
        }

        [Fact]
        public async Task CreateUser_SuccessfullyCreatesUser()
        {
            var name = "New User";
            var email = "newuser@email.com";
            var command = new CreateUserCommand(name, email);

            var result = await _mediator.Send(command);

            Assert.NotNull(result);
            Assert.Equal(name, result.Name);
            Assert.Equal(email, result.Email);

            var getResult = await _mediator.Send(new GetUserByEmailQuery(email));
            Assert.NotNull(getResult);
            Assert.Equal(name, getResult.Name);
        }

        [Fact]
        public async Task CreateUser_ThrowsDuplicateEmailException_WhenEmailExists()
        {
            var name = "John Doe";
            var email = "johndoe@email.com";
            var command = new CreateUserCommand(name, email);

            await Assert.ThrowsAsync<DuplicateEmailException>(async () =>
            {
                await _mediator.Send(command);
            });
        }
    }
}