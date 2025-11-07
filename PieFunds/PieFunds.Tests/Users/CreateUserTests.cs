using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PieFunds.Application.Features.Users.Commands.CreateUser;
using PieFunds.Application.Features.Users.Queries.GetUserByEmail;
using PieFunds.Application.Interfaces;
using PieFunds.Infrastructure.Persistence;
using PieFunds.Tests.TestInfrastructure;

namespace PieFunds.Tests.Users
{
    public class CreateUserTests : IClassFixture<MediatorFixture>, IAsyncLifetime
    {
        private readonly IMediator _mediator;
        private readonly MediatorFixture _fixture;

        public CreateUserTests(MediatorFixture fixture)
        {
            _fixture = fixture;
            _mediator = fixture.Mediator;
        }

        public async Task InitializeAsync()
        {
            var repo = (InMemoryUserRepository)_fixture.ServiceProvider.GetRequiredService<IUserRepository>();
            repo.Clear();
            await Task.CompletedTask;
        }
        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task CreateUser_SuccessfullyCreatesUser()
        {
            var name = "New User";
            var email = "newuser@email.com";
            var command = new CreateUserCommand(name, email);

            var result = await _mediator.Send(command);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("User created successfully.", result.Message);
            Assert.NotNull(result.User);
            Assert.Equal(name, result.User.Name);
            Assert.Equal(email, result.User.Email);

            var getResult = await _mediator.Send(new GetUserByEmailQuery(email));
            Assert.NotNull(getResult);
            Assert.Equal(name, getResult.Data?.Name);
        }

        [Fact]
        public async Task CreateUser_ReturnsFailure_WhenEmailExists()
        {
            var name = "John Doe";
            var email = "johndoe@email.com";
            var command = new CreateUserCommand(name, email);

            // First creation should succeed
            var firstResult = await _mediator.Send(command);
            Assert.True(firstResult.Success);

            // Second creation should fail
            var secondResult = await _mediator.Send(command);
            Assert.False(secondResult.Success);
            Assert.Equal("Email already registered.", secondResult.Message);
            Assert.Null(secondResult.User);
        }
    }
}