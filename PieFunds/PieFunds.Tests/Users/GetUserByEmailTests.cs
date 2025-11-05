using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PieFunds.Application.UserFeature.Queries;
using PieFunds.Tests.TestInfrastructure;

namespace PieFunds.Tests.Users
{
    public class GetUserByEmailTests : IClassFixture<MediatorFixture>
    {
        private readonly IMediator _mediator;

        public GetUserByEmailTests(MediatorFixture fixture)
        {
            _mediator = fixture.Mediator;
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var existingUserEmail = "johndoe@email.com";

            // Act
            var result = await _mediator.Send(new GetUserByEmailQuery(existingUserEmail));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingUserEmail, result.Email);
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            var nonExistingUserEmail = "emailNotExist@email.com";

            // Act
            var result = await _mediator.Send(new GetUserByEmailQuery(nonExistingUserEmail));

            // Assert
            Assert.Null(result);

        }

    }
}